import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component } from '@angular/core';
import { AuthService } from '@app/auth/core/auth.service';
import { InstallationIdService } from '@app/auth/core/installation-id.service';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';

@Component({
  selector: 'rolx-main',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
})
export class MainPageComponent {
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset).pipe(
    map((result) => result.matches),
    shareReplay(),
  );

  constructor(
    public readonly authService: AuthService,
    private readonly breakpointObserver: BreakpointObserver,
    public readonly installationIdService: InstallationIdService,
  ) {}
}
