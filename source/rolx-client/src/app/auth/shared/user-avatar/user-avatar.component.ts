import { Component } from '@angular/core';
import { AuthService } from '@app/auth/core';

@Component({
  selector: 'rolx-user-avatar',
  templateUrl: './user-avatar.component.html',
  styleUrls: ['./user-avatar.component.scss'],
})
export class UserAvatarComponent {

  constructor(public authService: AuthService) { }

}
