import { Injectable, NgZone } from '@angular/core';
import { BehaviorSubject, bindCallback, forkJoin, from, Observable, of, Subject } from 'rxjs';
import { catchError, filter, map, switchMap, take } from 'rxjs/operators';

import { enterZone } from '@app/core/util';
import { CurrentUser } from './current-user';
import { Info } from './info';
import { SignInState } from './sign-in-state';
import { SignInService } from './sign-in.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private googleUserSubject = new Subject<gapi.auth2.GoogleUser>();
  private currentUserSubject = new BehaviorSubject<CurrentUser>(new CurrentUser());
  currentUser$ = this.currentUserSubject.asObservable();

  constructor(private signInService: SignInService,
              private zone: NgZone) {
    console.log('--- AuthService.ctor()');

    this.currentUser$.pipe(
      filter(u => u.state === SignInState.Authenticated),
      switchMap(u => this.signInService.signIn(u).pipe(
        map(au => CurrentUser.fromAuthenticatedUser(au)),
        catchError(e => this.handleSignInError(e))
      ))
    ).subscribe(u => this.currentUserSubject.next(u));

    this.googleUserSubject.subscribe(u => this.currentUserSubject.next(CurrentUser.fromGoogleUser(u)));

    this.currentUser$.subscribe(u => console.log('Current user changed:', u));
  }

  static Initializer(authService: AuthService) {
    return () => authService.initialize();
  }

  get currentUser() {
    return this.currentUserSubject.value;
  }

  renderSignInButton(targetElementId: string) {
    gapi.signin2.render(targetElementId, {
      scope: 'profile email',
      width: 240,
      height: 50,
      longtitle: true,
      theme: 'dark',
      onfailure: reason => console.warn('Failed to authenticate with google. Reason:', reason),
    });
  }

  signOut(): Observable<void> {
    return from(gapi.auth2.getAuthInstance().disconnect())
      .pipe(
        enterZone(this.zone),
        map(() => this.currentUserSubject.next(new CurrentUser()))
      );
  }

  private initialize(): Promise<void> {
    console.log('--- AuthService.initialize()');

    const firstGoogleUser = this.googleUserSubject.pipe(
      take(1)
    );

    const gapiLoader = bindCallback(gapi.load);
    const initialization = this.signInService.getInfo().pipe(
      switchMap(info => gapiLoader('auth2').pipe(
        map(() => this.initializeGApi(info))
      ))
    );

    return forkJoin(firstGoogleUser, initialization)
      .toPromise().then(() => console.log('--- AuthService.initialize() done'));
  }

  private initializeGApi(info: Info) {
    gapi.auth2.init({
      client_id: info.googleClientId,
    });

    const auth2 = gapi.auth2.getAuthInstance();
    auth2.currentUser.listen(u => this.zone.run(() => this.googleUserSubject.next(u)));
    console.log(auth2.currentUser.get());
  }

  private handleSignInError(error: any) {
    console.warn('Failed to sign in', error);
    return of(new CurrentUser());
  }
}
