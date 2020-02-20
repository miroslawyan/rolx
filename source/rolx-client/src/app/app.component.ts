import { OverlayContainer } from '@angular/cdk/overlay';
import { Component, HostBinding, OnInit } from '@angular/core';
import { Theme, ThemeService } from './core/theme';

@Component({
  selector: 'rolx-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {

  private static readonly ThemeClasses = ['dark-theme', 'bright-theme'];

  @HostBinding('class') componentCssClass;

  constructor(public themeService: ThemeService, public overlayContainer: OverlayContainer) { }

  ngOnInit() {
    this.themeService.currentTheme$
      .subscribe(t => this.applyTheme(t));
  }

  private applyTheme(theme: Theme) {
    const themeClass = AppComponent.ThemeClasses[theme];
    this.componentCssClass = themeClass;

    const overlayClassList = this.overlayContainer.getContainerElement().classList;
    overlayClassList.remove(...AppComponent.ThemeClasses);
    overlayClassList.add(themeClass);
  }
}
