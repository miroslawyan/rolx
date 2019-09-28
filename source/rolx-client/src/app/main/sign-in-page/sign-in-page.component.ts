import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';

import { AuthService, SignInState } from '@app/core/auth';

@Component({
  selector: 'rolx-sign-in',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.scss']
})
export class SignInPageComponent implements OnInit, AfterViewInit, OnDestroy {

  SignInState = SignInState;

  private forwardRoute = '';
  private subscriptions: Subscription[] = [];

  constructor(public authService: AuthService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.forwardRoute = this.route.snapshot.queryParams.forwardRoute || '/';

    this.subscriptions.push(this.authService.currentUser$.pipe(
      filter(u => u.state === SignInState.SignedIn)
    ).subscribe(() => this.navigateForward()));
  }

  ngAfterViewInit() {
    this.authService.renderSignInButton('sign-in-button');
  }

  ngOnDestroy(): void {
    this.subscriptions.map(s => s.unsubscribe());
  }

  private navigateForward() {
    this.router.navigateByUrl(this.forwardRoute)
      .catch( (e: Error) => {
        console.log(e.name + ': ' + e.message);
      });
  }

}
