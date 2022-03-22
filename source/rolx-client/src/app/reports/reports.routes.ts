import { Routes } from '@angular/router';
import { Role } from '@app/auth/core/role';
import { RoleGuard } from '@app/auth/core/role.guard';

import { ExportPageComponent } from './pages/export-page/export-page.component';
import { UserMonthReportPageComponent } from './pages/user-month-report-page/user-month-report-page.component';

export const ReportsRoutes: Routes = [
  {
    path: 'export',
    component: ExportPageComponent,
    canActivate: [RoleGuard],
    data: { allowedRoles: [Role.Supervisor, Role.Administrator] },
  },
  {
    path: 'reports/user/:year/:month',
    component: UserMonthReportPageComponent,
  },
  {
    path: 'reports/user',
    component: UserMonthReportPageComponent,
  },
];
