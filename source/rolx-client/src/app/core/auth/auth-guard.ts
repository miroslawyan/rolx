import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';

import { AuthService } from './auth.service';
import { SignInState } from './sign-in-state';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const currentUser = this.authService.currentUser;

    if (currentUser.state !== SignInState.SignedIn) {
      // noinspection JSIgnoredPromiseFromCall
      this.router.navigate(['/sign-in'], { queryParams: { forwardRoute: state.url } });
      return false;
    }

    return true;
  }
}
