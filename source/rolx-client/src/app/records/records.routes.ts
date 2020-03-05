import { Routes } from '@angular/router';
import {
  WeekPageComponent,
} from './pages';

export const RecordsRoutes: Routes = [
  {
    path: 'week/:date',
    component: WeekPageComponent,
  },
  {
    path: 'week',
    component: WeekPageComponent,
  },
];
