import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { UserEditPageComponent, UserListPageComponent } from './pages';
import { UserFormComponent, UserTableComponent } from './shared';

@NgModule({
  imports: [
    AppImportModule,
    CommonModule,
  ],
  declarations: [
    UserEditPageComponent,
    UserFormComponent,
    UserListPageComponent,
    UserTableComponent,
  ],
})
export class UserManagementModule { }
