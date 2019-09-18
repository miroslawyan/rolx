import { AfterViewInit, Component } from '@angular/core';

import { AuthService, SignInState } from '@app/core/auth';

@Component({
  selector: 'rolx-sign-in',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.scss']
})
export class SignInPageComponent implements AfterViewInit {

  SignInState = SignInState;

  constructor(public authService: AuthService) { }

  ngAfterViewInit() {
    this.authService.renderSignInButton('sign-in-button');
  }

}
