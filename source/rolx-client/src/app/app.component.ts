import { Component } from '@angular/core';
import { map } from 'rxjs/operators';
import { Theme, ThemeService } from './core/theme';

@Component({
  selector: 'rolx-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {

  themeClass$ = this.themeService.currentTheme$.pipe(
    map(t => t === Theme.bright ? 'bright-theme' : 'dark-theme'),
  );

  constructor(public themeService: ThemeService) { }

}
