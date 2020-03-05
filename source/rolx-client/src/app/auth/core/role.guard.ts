import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';
import { Role } from './role';

@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {
  constructor(
    private router: Router,
    private authService: AuthService,
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const currentRole = this.authService.currentApproval?.user.role || Role.User;
    const allowedRoles = route.data.allowedRoles as Role[] || [];

    if (!allowedRoles.includes(currentRole)) {
      return this.router.createUrlTree(['/']);
    }

    return true;
  }
}
