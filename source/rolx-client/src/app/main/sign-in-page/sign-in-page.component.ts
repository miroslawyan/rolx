import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService, SignInState } from '@app/core/auth';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'rolx-sign-in',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.scss'],
})
export class SignInPageComponent implements OnInit, AfterViewInit, OnDestroy {

  SignInState = SignInState;

  private forwardRoute = '';
  private subscriptions: Subscription[] = [];

  constructor(private authService: AuthService,
              private route: ActivatedRoute,
              private router: Router) { }

  get currentUser() {
    return this.authService.currentUser;
  }

  ngOnInit(): void {
    this.forwardRoute = this.route.snapshot.queryParams.forwardRoute || '/';

    this.subscriptions.push(this.authService.currentUser$.pipe(
      filter(u => u.state === SignInState.SignedIn),
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
      .catch(e => console.log(e.name + ': ' + e.message));
  }

}
