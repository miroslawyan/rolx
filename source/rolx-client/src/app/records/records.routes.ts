import { Routes } from '@angular/router';
import { WeekPageComponent } from '@app/records/pages/week-page/week-page.component';
import { YearOverviewPageComponent } from '@app/records/pages/year-overview-page/year-overview-page.component';

export const RecordsRoutes: Routes = [
  {
    path: 'week/:date',
    component: WeekPageComponent,
  },
  {
    path: 'week',
    redirectTo: 'week/',
    pathMatch: 'full',
  },
  {
    path: 'year-overview/:date',
    component: YearOverviewPageComponent,
  },
  {
    path: 'year-overview',
    component: YearOverviewPageComponent,
  },
];
