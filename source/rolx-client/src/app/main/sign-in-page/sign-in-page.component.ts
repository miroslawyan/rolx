import { AfterViewInit, Component } from '@angular/core';

import { AuthService } from '@app/core/auth/auth.service';

@Component({
  selector: 'rolx-sign-in',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.scss']
})
export class SignInPageComponent implements AfterViewInit {

  constructor(private authService: AuthService) { }

  ngAfterViewInit() {
    this.authService.renderSignInButton('sign-in-button');
  }

  signOut() {
    this.authService.signOut();
  }

}
