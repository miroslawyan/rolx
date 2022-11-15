import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@app/auth/core/auth.service';
import { GoogleAuthService } from '@app/auth/core/google-auth.service';
import { InstallationIdService } from '@app/auth/core/installation-id.service';
import { PendingRequestService } from '@app/core/pending-request/pending-request.service';
import { assertDefined } from '@app/core/util/utils';
import { Subscription } from 'rxjs';

@Component({
  selector: 'rolx-sign-in',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.scss'],
})
export class SignInPageComponent implements OnInit, AfterViewInit, OnDestroy {
  private readonly subscriptions = new Subscription();
  private forwardRoute = '';

  @ViewChild('signInButton') signInButton!: ElementRef;

  constructor(
    private readonly authService: AuthService,
    private readonly googleAuthService: GoogleAuthService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly pendingRequestService: PendingRequestService,
    public readonly installationIdService: InstallationIdService,
  ) {}

  ngOnInit(): void {
    this.forwardRoute = this.route.snapshot.queryParams['forwardRoute'] || '/';

    this.subscriptions.add(
      this.googleAuthService.$credentials.subscribe(async (c) => await this.signIn(c)),
    );
  }

  ngAfterViewInit(): void {
    assertDefined(this, 'signInButton');

    Promise.all([this.googleAuthService.initialize(), this.authService.initialize()]).then(() => {
      this.googleAuthService.renderButton(this.signInButton);
    });
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  private async signIn(googleIdToken: string): Promise<void> {
    this.pendingRequestService.requestStarted();

    try {
      await this.authService.signIn(googleIdToken);
    } finally {
      this.pendingRequestService.requestFinished();
    }

    await this.router.navigateByUrl(this.forwardRoute);
  }
}
