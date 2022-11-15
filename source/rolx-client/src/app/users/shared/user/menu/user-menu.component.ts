import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@app/auth/core/auth.service';
import { Theme } from '@app/core/theme/theme';
import { ThemeService } from '@app/core/theme/theme.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'rolx-user-menu',
  templateUrl: './user-menu.component.html',
  styleUrls: ['./user-menu.component.scss'],
})
export class UserMenuComponent {
  nextTheme$ = this.themeService.currentTheme$.pipe(
    map((t) => ({
      name: t === Theme.Bright ? 'Dark' : 'Bright',
      icon: t === Theme.Bright ? 'dark_mode' : 'light_mode',
    })),
  );

  constructor(
    private authService: AuthService,
    private router: Router,
    private themeService: ThemeService,
  ) {}

  get user() {
    return this.authService.currentApproval?.user;
  }

  signOut() {
    this.authService.signOut();

    // noinspection JSIgnoredPromiseFromCall
    this.router.navigateByUrl('/sign-in');
  }

  toggleTheme() {
    this.themeService.theme = this.themeService.theme === Theme.Dark ? Theme.Bright : Theme.Dark;
  }
}
