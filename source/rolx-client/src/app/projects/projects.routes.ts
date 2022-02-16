import { Routes } from '@angular/router';
import { ActivityEditPageComponent } from '@app/projects/pages/activity-edit-page/activity-edit-page.component';
import { SubprojectDetailPageComponent } from '@app/projects/pages/subproject-detail-page/subproject-detail-page.component';
import { SubprojectEditPageComponent } from '@app/projects/pages/subproject-edit-page/subproject-edit-page.component';
import { SubprojectListPageComponent } from '@app/projects/pages/subproject-list-page/subproject-list-page.component';

export const ProjectsRoutes: Routes = [
  {
    path: 'subproject',
    component: SubprojectListPageComponent,
  },
  {
    path: 'subproject/:id',
    component: SubprojectDetailPageComponent,
  },
  {
    path: 'subproject/:id/edit',
    component: SubprojectEditPageComponent,
  },
  {
    path: 'subproject/:id/activity/:activityId',
    component: ActivityEditPageComponent,
  },
];
