import { Routes } from '@angular/router';
import {
  PhaseEditPageComponent,
  ProjectDetailPageComponent,
  ProjectEditPageComponent,
  ProjectListPageComponent,
} from './pages';

export const ProjectsRoutes: Routes = [
  {
    path: 'project',
    component: ProjectListPageComponent,
  },
  {
    path: 'project/:id',
    component: ProjectDetailPageComponent,
  },
  {
    path: 'project/:id/edit',
    component: ProjectEditPageComponent,
  },
  {
    path: 'project/:id/phase/:phaseId',
    component: PhaseEditPageComponent,
  },
];
