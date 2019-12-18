import { Component, ViewChild } from '@angular/core';
import { MatMenu } from '@angular/material';
import { Router } from '@angular/router';
import { AuthService } from '@app/auth/core';

@Component({
  selector: 'rolx-user-menu',
  templateUrl: './user-menu.component.html',
  styleUrls: ['./user-menu.component.scss'],
  exportAs: 'rolxUserMenu',
})
export class UserMenuComponent {

  constructor(private authService: AuthService, private router: Router) { }

  @ViewChild(MatMenu, {static: true}) open: MatMenu;

  signOut() {
    this.authService.signOut()
      .subscribe(() => this.router.navigateByUrl('/sign-in')
        .catch(e => console.log(e.name + ': ' + e.message)));
  }
}
