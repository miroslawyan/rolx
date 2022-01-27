import { Component, Input } from '@angular/core';
import { User } from '@app/users/core/user';

@Component({
  selector: 'rolx-user-avatar',
  templateUrl: './user-avatar.component.html',
})
export class UserAvatarComponent {

  private static readonly defaultAvatar = 'assets/images/defaultAvatar.png';

  private hadError = false;

  @Input()
  user: User;

  get url(): string {
    return this.hadError
      ? UserAvatarComponent.defaultAvatar
      : this.user?.avatarUrl ?? UserAvatarComponent.defaultAvatar;
  }

  handleError() {
    this.hadError = true;
  }

}
