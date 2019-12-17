import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { UserAvatarComponent } from './shared/user-avatar/user-avatar.component';

@NgModule({
  imports: [
    CommonModule,
    AppImportModule,
  ],
  declarations: [UserAvatarComponent],
  exports: [
    UserAvatarComponent,
  ],
})
export class AuthModule { }
