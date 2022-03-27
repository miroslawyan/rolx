import { Routes } from '@angular/router';
import { RoleGuard } from '@app/auth/core/role.guard';
import { Role } from '@app/users/core/role';

import { ExportPageComponent } from './pages/export-page/export-page.component';
import { UserMonthReportPageComponent } from './pages/user-month-report-page/user-month-report-page.component';

export const ReportsRoutes: Routes = [
  {
    path: 'export',
    component: ExportPageComponent,
    canActivate: [RoleGuard],
    data: { minRole: Role.Supervisor },
  },
  {
    path: 'reports/:month',
    component: UserMonthReportPageComponent,
  },
  {
    path: 'reports',
    redirectTo: 'reports/',
    pathMatch: 'full',
  },
];
