import { Component } from '@angular/core';
import { AuthService } from '@app/auth/core/auth.service';

@Component({
  selector: 'rolx-forbidden-page',
  templateUrl: './forbidden-page.component.html',
  styleUrls: ['./forbidden-page.component.scss'],
})
export class ForbiddenPageComponent {
  constructor(authService: AuthService) {
    authService.signOut();
  }
}
