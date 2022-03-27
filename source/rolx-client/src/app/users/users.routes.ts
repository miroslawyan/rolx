import { Routes } from '@angular/router';
import { RoleGuard } from '@app/auth/core/role.guard';
import { Role } from '@app/users/core/role';
import { UserEditPageComponent } from '@app/users/pages/user-edit-page/user-edit-page.component';
import { UserListPageComponent } from '@app/users/pages/user-list-page/user-list-page.component';

export const UsersRoutes: Routes = [
  {
    path: 'user',
    component: UserListPageComponent,
    canActivate: [RoleGuard],
    data: { minRole: Role.Supervisor },
  },
  {
    path: 'user/:id',
    component: UserEditPageComponent,
    canActivate: [RoleGuard],
    data: { minRole: Role.Administrator },
  },
];
