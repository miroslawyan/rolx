import { AfterViewInit, Component, NgZone } from '@angular/core';


@Component({
  selector: 'rolx-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements AfterViewInit {

  constructor(private zone: NgZone) { }

  ngAfterViewInit() {
    gapi.signin2.render('sign-in-button', {
      scope: 'profile email',
      width: 240,
      height: 50,
      longtitle: true,
      theme: 'dark',
      onsuccess: param => this.zone.run(() => this.onSuccess(param))
    });
  }

  signOut() {
    const auth2 = gapi.auth2.getAuthInstance();
    auth2.signOut().then(() => console.log('User signed out.'));
  }

  private onSuccess(googleUser) {
    console.log(googleUser.getBasicProfile());
  }

}
