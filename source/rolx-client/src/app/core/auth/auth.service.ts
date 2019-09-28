import { Injectable, NgZone } from '@angular/core';
import { BehaviorSubject, bindCallback } from 'rxjs';
import { map, switchMap, tap } from 'rxjs/operators';

import { Info } from './info';
import { SignInService } from './sign-in.service';
import { User } from './user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSubject = new BehaviorSubject<User>(new User());

  currentUser$ = this.currentUserSubject.pipe(
    tap(u => console.log('Current user:', u))
  );

  constructor(private signInService: SignInService,
              private zone: NgZone) {
  }

  static Initializer(authService: AuthService) {
    return () => authService.initialize();
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
    gapi.auth2.getAuthInstance().signOut().then(() => console.log('User signed out.'));
  }

  disconnectFromGoogle() {
    gapi.auth2.getAuthInstance()
      .disconnect()
      .then(() => this.zone.run(() => this.currentUserSubject.next(new User())));
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
    auth2.currentUser.listen(u => this.zone.run(() => this.currentUserSubject.next(User.fromGoogleUser(u))));
  }

  private onSuccess(googleUser: gapi.auth2.GoogleUser) {
    console.log('Success');
    console.log(googleUser.getBasicProfile());
  }

  private onFailure(error: string) {
    console.warn('Failed to authenticate with google. Error:', error);
  }
}
