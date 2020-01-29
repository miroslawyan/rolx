import { Routes } from '@angular/router';
import { UserManagementGuard } from './core/user-management-guard';
import { UserListPageComponent } from './pages';

export const UserManagementRoutes: Routes = [
  {
    path: 'user',
    canActivate: [UserManagementGuard],
    component: UserListPageComponent,
  },
];
