import { Routes } from '@angular/router';
import { UserMonthReportPageComponent } from '@app/reports/pages/user-month-report-page/user-month-report-page.component';

export const ReportsRoutes: Routes = [
  {
    path: 'reports/user/:year/:month',
    component: UserMonthReportPageComponent,
  },
  {
    path: 'reports/user',
    component: UserMonthReportPageComponent,
  },
];
