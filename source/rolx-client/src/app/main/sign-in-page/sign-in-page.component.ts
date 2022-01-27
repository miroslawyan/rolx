import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@app/auth/core/auth.service';
import { GoogleAuthService } from '@app/auth/core/google-auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'rolx-sign-in',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.scss'],
})
export class SignInPageComponent implements OnInit, OnDestroy {

  private readonly subscriptions = new Subscription();
  private forwardRoute = '';

  busy = false;

  constructor(private authService: AuthService,
              private googleAuthService: GoogleAuthService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.forwardRoute = this.route.snapshot.queryParams.forwardRoute || '/';

    this.busy = true;
    Promise.all([
      this.googleAuthService.initialize(),
      this.authService.initialize(),
    ]).then(() => {
      this.googleAuthService.signOut();
      this.busy = false;
    });
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
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
