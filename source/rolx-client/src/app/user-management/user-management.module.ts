import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { UserListPageComponent } from './pages';
import { UserTableComponent } from './shared/table/user-table.component';

@NgModule({
  imports: [
    AppImportModule,
    CommonModule,
  ],
  declarations: [
    UserListPageComponent,
    UserTableComponent,
  ],
})
export class UserManagementModule { }
