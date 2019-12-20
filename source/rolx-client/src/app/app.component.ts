import { Component } from '@angular/core';
import { map } from 'rxjs/operators';
import { Theme, ThemeService } from './core/theme';

@Component({
  selector: 'rolx-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {

  themeClass$ = this.themeService.currentTheme$.pipe(map(this.themeClassMapper));

  constructor(public themeService: ThemeService) { }

  themeClassMapper(theme: Theme): string {
    switch (theme) {
      case Theme.bright:
        return 'bright-theme';
      case Theme.dark:
        return 'dark-theme';
      default:
        console.error('Unknown theme.');
    }
  }

}
