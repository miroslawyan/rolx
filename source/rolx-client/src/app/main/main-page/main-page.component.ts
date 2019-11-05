import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@app/core/auth';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';

@Component({
  selector: 'rolx-main',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
})
export class MainPageComponent {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay(),
    );

  constructor(public authService: AuthService,
              private breakpointObserver: BreakpointObserver,
              private router: Router) {}

  signOut() {
    this.authService.signOut()
      .subscribe(() => this.router.navigateByUrl('/sign-in')
        .catch(e => console.log(e.name + ': ' + e.message)));
  }

}
