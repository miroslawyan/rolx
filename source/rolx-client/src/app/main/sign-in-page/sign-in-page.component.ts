import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@app/auth/core/auth.service';
import { GoogleAuthService } from '@app/auth/core/google-auth.service';
import { Info } from '@app/auth/core/info';
import { SignInService } from '@app/auth/core/sign-in.service';

@Component({
  selector: 'rolx-sign-in',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.scss'],
})
export class SignInPageComponent implements OnInit {
  private forwardRoute = '';
  busy = false;
  info?: Info;

  get installationId(): string {
    if (this.info !== undefined && this.info.installationId !== 'Production') {
      return ' - ' + this.info.installationId;
    }

    return '';
  }

  constructor(
    private readonly authService: AuthService,
    private readonly googleAuthService: GoogleAuthService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    signInService: SignInService,
  ) {
    signInService.getInfo().subscribe((i) => (this.info = i));
  }

  ngOnInit(): void {
    this.forwardRoute = this.route.snapshot.queryParams['forwardRoute'] || '/';

    this.busy = true;
    Promise.all([this.googleAuthService.initialize(), this.authService.initialize()]).then(() => {
      this.googleAuthService.signOut();
      this.busy = false;
    });
  }

  async signIn() {
    this.busy = true;

    try {
      const googleUser = await this.googleAuthService.signIn();
      await this.authService.signIn(googleUser.getAuthResponse().id_token);
      await this.router.navigateByUrl(this.forwardRoute);
    } finally {
      this.busy = false;
    }
  }
}
