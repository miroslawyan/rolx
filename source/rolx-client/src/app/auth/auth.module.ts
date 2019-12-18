import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { UserAvatarComponent } from './shared/user-avatar/user-avatar.component';
import { UserMenuComponent } from './shared/user-menu/user-menu.component';

@NgModule({
  imports: [
    CommonModule,
    AppImportModule,
  ],
  declarations: [UserAvatarComponent, UserMenuComponent],
  exports: [
    UserAvatarComponent,
  ],
})
export class AuthModule { }
