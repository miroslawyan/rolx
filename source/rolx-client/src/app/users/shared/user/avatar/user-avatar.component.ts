import { Component, Input, OnInit } from '@angular/core';
import { assertDefined } from '@app/core/util/utils';
import { User } from '@app/users/core/user';

@Component({
  selector: 'rolx-user-avatar',
  templateUrl: './user-avatar.component.html',
})
export class UserAvatarComponent implements OnInit {
  private static readonly defaultAvatar = 'assets/images/defaultAvatar.png';

  private hadError = false;

  @Input()
  user!: User;

  get url(): string {
    return this.hadError
      ? UserAvatarComponent.defaultAvatar
      : this.user?.avatarUrl ?? UserAvatarComponent.defaultAvatar;
  }

  ngOnInit(): void {
    assertDefined(this, 'user');
  }

  handleError() {
    this.hadError = true;
  }
}
