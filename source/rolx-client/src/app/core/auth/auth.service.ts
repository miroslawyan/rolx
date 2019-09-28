import { Injectable, NgZone } from '@angular/core';
import { BehaviorSubject, bindCallback } from 'rxjs';
import { filter, map, switchMap, tap } from 'rxjs/operators';

import { CurrentUser } from './current-user';
import { Info } from './info';
import { SignInState } from './sign-in-state';
import { SignInService } from './sign-in.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSubject = new BehaviorSubject<CurrentUser>(new CurrentUser());
  currentUser$ = this.currentUserSubject.asObservable();

  constructor(private signInService: SignInService,
              private zone: NgZone) {
    this.currentUser$.pipe(
      filter(u => u.state === SignInState.Authenticated),
      switchMap(u => this.signInService.signIn(u)),
      map(u => CurrentUser.fromAuthenticatedUser(u))
    ).subscribe(u => this.currentUserSubject.next(u));

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
      onsuccess: googleUser => this.zone.run(() => this.onSuccess(googleUser)),
      onfailure: reason => this.zone.run(() => this.onFailure(reason.error)),
    });
  }

  signOut() {
    gapi.auth2.getAuthInstance().signOut().then(() => console.log('CurrentUser signed out.'));
  }

  disconnectFromGoogle() {
    gapi.auth2.getAuthInstance()
      .disconnect()
      .then(() => this.zone.run(() => this.currentUserSubject.next(new CurrentUser())));
  }

  private initialize(): Promise<void> {
    const gapiLoader = bindCallback(gapi.load);
    return this.signInService.getInfo().pipe(
      switchMap(info => gapiLoader('auth2').pipe(
        map(() => this.initializeGApi(info))
      ))
    ).toPromise();
  }

  private initializeGApi(info: Info) {
    gapi.auth2.init({
      client_id: info.googleClientId,
    });

    const auth2 = gapi.auth2.getAuthInstance();
    auth2.currentUser.listen(u => this.zone.run(() => this.currentUserSubject.next(CurrentUser.fromGoogleUser(u))));
  }

  private onSuccess(googleUser: gapi.auth2.GoogleUser) {
    console.log('Success');

  }

  private onFailure(error: string) {
    console.warn('Failed to authenticate with google. Error:', error);
  }
}
