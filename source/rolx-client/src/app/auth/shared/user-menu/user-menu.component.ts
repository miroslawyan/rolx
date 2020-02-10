import { Component, ViewChild } from '@angular/core';
import { MatMenu } from '@angular/material/menu';
import { Router } from '@angular/router';
import { AuthService } from '@app/auth/core';
import { Theme, ThemeService } from '@app/core/theme';

@Component({
  selector: 'rolx-user-menu',
  templateUrl: './user-menu.component.html',
  styleUrls: ['./user-menu.component.scss'],
  exportAs: 'rolxUserMenu',
})
export class UserMenuComponent {

  constructor(private authService: AuthService, private router: Router, public themeService: ThemeService) { }

  @ViewChild(MatMenu, {static: true}) open: MatMenu;

  Theme = Theme;

  signOut() {
    this.authService.signOut()
      .subscribe(() => this.router.navigateByUrl('/sign-in')
        .catch(e => console.log(e.name + ': ' + e.message)));
  }

}
