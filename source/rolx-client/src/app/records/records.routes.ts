import { Routes } from '@angular/router';
import {
  MonthPageComponent,
  WeekPageComponent,
} from './pages';

export const RecordsRoutes: Routes = [
  {
    path: '',
    component: MonthPageComponent,
  },
  {
    path: 'week/:date',
    component: WeekPageComponent,
  },
  {
    path: 'week',
    component: WeekPageComponent,
  },
];
