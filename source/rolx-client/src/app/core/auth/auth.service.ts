import { Injectable, NgZone } from '@angular/core';

import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private zone: NgZone) {
    gapi.load('auth2', () => {
      // Retrieve the singleton for the GoogleAuth library and set up the client.
      gapi.auth2.init({
        client_id: environment.googleClientId,
        cookie_policy: 'single_host_origin',
      });
    });
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

  private onSuccess(googleUser: gapi.auth2.GoogleUser) {
    console.log('Success');
    console.log(googleUser.getBasicProfile());
  }

  private onFailure(error: string) {
    console.warn('Failed to authenticate with google. Error:', error);
  }
}
