import { Routes } from '@angular/router';
import { Role, RoleGuard } from '@app/auth/core';
import { UserEditPageComponent, UserListPageComponent } from './pages';

export const UserManagementRoutes: Routes = [
  {
    path: 'user',
    component: UserListPageComponent,
    canActivate: [RoleGuard],
    data: {allowedRoles: [Role.Supervisor, Role.Administrator]},
  },
  {
    path: 'user/:id',
    component: UserEditPageComponent,
    canActivate: [RoleGuard],
    data: {allowedRoles: [Role.Administrator]},
  },
];
