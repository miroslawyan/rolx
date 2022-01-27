import { Routes } from '@angular/router';
import { PhaseEditPageComponent } from '@app/projects/pages/phase-edit-page/phase-edit-page.component';
import { ProjectDetailPageComponent } from '@app/projects/pages/project-detail-page/project-detail-page.component';
import { ProjectEditPageComponent } from '@app/projects/pages/project-edit-page/project-edit-page.component';
import { ProjectListPageComponent } from '@app/projects/pages/project-list-page/project-list-page.component';

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
