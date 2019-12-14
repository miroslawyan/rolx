import { Routes } from '@angular/router';
import {
  MonthPageComponent,
  WeekPageComponent,
} from './pages';

export const WorkRecordRoutes: Routes = [
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
