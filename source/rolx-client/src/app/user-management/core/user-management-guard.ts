import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService, Role } from '@app/auth/core';

@Injectable({ providedIn: 'root' })
export class UserManagementGuard implements CanActivate {
  constructor(private router: Router, private authService: AuthService) { }

  canActivate() {
    if (this.authService.currentUser.role >= Role.Supervisor) {
      return true;
    } else {
      return this.router.createUrlTree(['/']);
    }
  }

}
