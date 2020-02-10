import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Theme } from './theme';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {

  private currentTheme = new BehaviorSubject<Theme>(ThemeService.loadThemeFromStorage());

  currentTheme$ = this.currentTheme.asObservable();

  private static loadThemeFromStorage(): Theme {
    const storedTheme = localStorage.getItem('theme') ?? '';
    const keyEnum = Theme[storedTheme];
    if (keyEnum != null) {
      return keyEnum;
    } else {
      const defaultTheme = Theme.dark;
      localStorage.setItem('theme', Theme[defaultTheme]);
      return defaultTheme;
    }
  }

  get theme(): Theme {
    return this.currentTheme.value;
  }

  set theme(theme: Theme) {
    localStorage.setItem('theme', Theme[theme]);
    setTimeout(() => this.currentTheme.next(theme), 200);
  }
}
