import { Routes } from '@angular/router';
import { Role } from '@app/auth/core/role';
import { RoleGuard } from '@app/auth/core/role.guard';
import { UserEditPageComponent } from '@app/users/pages/user-edit-page/user-edit-page.component';
import { UserListPageComponent } from '@app/users/pages/user-list-page/user-list-page.component';

export const UsersRoutes: Routes = [
  {
    path: 'user',
    component: UserListPageComponent,
    canActivate: [RoleGuard],
    data: { allowedRoles: [Role.Supervisor, Role.Administrator] },
  },
  {
    path: 'user/:id',
    component: UserEditPageComponent,
    canActivate: [RoleGuard],
    data: { allowedRoles: [Role.Administrator] },
  },
];
