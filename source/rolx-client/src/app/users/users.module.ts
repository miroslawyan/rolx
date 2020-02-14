import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { UserEditPageComponent, UserListPageComponent } from './pages';
import { UserAvatarComponent, UserFormComponent, UserMenuComponent, UserTableComponent } from './shared';

@NgModule({
  imports: [
    AppImportModule,
    CommonModule,
  ],
  exports: [
    UserAvatarComponent,
    UserMenuComponent,
  ],
  declarations: [
    UserAvatarComponent,
    UserEditPageComponent,
    UserFormComponent,
    UserListPageComponent,
    UserMenuComponent,
    UserTableComponent,
  ],
})
export class UsersModule { }
