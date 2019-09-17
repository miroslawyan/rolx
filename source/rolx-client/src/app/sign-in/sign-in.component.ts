import { AfterViewInit, Component } from '@angular/core';


@Component({
  selector: 'rolx-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements AfterViewInit {

  constructor() { }

  ngAfterViewInit() {
    gapi.signin2.render('sign-in-button', {
      scope: 'profile email',
      width: 240,
      height: 50,
      longtitle: true,
      theme: 'light',
      onsuccess: param => this.onSuccess(param) // TODO: call this in the zone
    });
  }

  onSuccess(googleUser) {
    console.log(googleUser.getBasicProfile());
  }

}
