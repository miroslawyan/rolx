import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Role } from '@app/users/core/role';

import { AuthService } from './auth.service';

@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {
  constructor(private router: Router, private authService: AuthService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const currentRole = this.authService.currentApproval?.user.role || Role.User;
    const minRole = (route.data['minRole'] as Role) ?? Role.Administrator;

    if (currentRole < minRole) {
      return this.router.createUrlTree(['/']);
    }

    return true;
  }
}
