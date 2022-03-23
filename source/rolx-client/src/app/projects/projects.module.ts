import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { ActivityEditPageComponent } from '@app/projects/pages/activity-edit-page/activity-edit-page.component';
import { SubprojectDetailPageComponent } from '@app/projects/pages/subproject-detail-page/subproject-detail-page.component';
import { SubprojectEditPageComponent } from '@app/projects/pages/subproject-edit-page/subproject-edit-page.component';
import { SubprojectListPageComponent } from '@app/projects/pages/subproject-list-page/subproject-list-page.component';
import { ActivityFormComponent } from '@app/projects/shared/activity/form/activity-form.component';
import { ActivitySelectorComponent } from '@app/projects/shared/activity/selector/activity-selector.component';
import { StarredActivityIndicatorComponent } from '@app/projects/shared/activity/starred-indicator/starred-activity-indicator.component';
import { ActivityTableComponent } from '@app/projects/shared/activity/table/activity-table.component';
import { SubprojectFormComponent } from '@app/projects/shared/subproject/form/subproject-form.component';
import { SubprojectTableComponent } from '@app/projects/shared/subproject/table/subproject-table.component';
import { ReportsModule } from '@app/reports/reports.module';

@NgModule({
  imports: [AppImportModule, CommonModule, ReportsModule],
  declarations: [
    ActivityEditPageComponent,
    SubprojectDetailPageComponent,
    SubprojectEditPageComponent,
    SubprojectListPageComponent,
    ActivityFormComponent,
    ActivitySelectorComponent,
    ActivityTableComponent,
    SubprojectFormComponent,
    SubprojectTableComponent,
    StarredActivityIndicatorComponent,
  ],
  exports: [
    ActivityEditPageComponent,
    SubprojectDetailPageComponent,
    SubprojectEditPageComponent,
    SubprojectListPageComponent,
    ActivitySelectorComponent,
    StarredActivityIndicatorComponent,
  ],
})
export class ProjectsModule {}
