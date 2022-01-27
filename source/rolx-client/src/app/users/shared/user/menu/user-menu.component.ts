import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@app/auth/core/auth.service';
import { Theme } from '@app/core/theme/theme';
import { ThemeService } from '@app/core/theme/theme.service';

@Component({
  selector: 'rolx-user-menu',
  templateUrl: './user-menu.component.html',
  styleUrls: ['./user-menu.component.scss'],
})
export class UserMenuComponent {

  constructor(private authService: AuthService,
              private router: Router,
              public themeService: ThemeService) { }

  get user() {
    return this.authService.currentApproval?.user;
  }

  get nextThemeName() {
    switch (this.themeService.theme) {
      case Theme.bright:
        return 'Dark';

      case Theme.dark:
      default:
        return 'Bright';
    }
  }

  signOut() {
    this.authService.signOut();

    // noinspection JSIgnoredPromiseFromCall
    this.router.navigateByUrl('/sign-in');
  }

  toggleTheme() {
    this.themeService.theme = this.themeService.theme === Theme.dark
      ? Theme.bright
      : Theme.dark;
  }

}
