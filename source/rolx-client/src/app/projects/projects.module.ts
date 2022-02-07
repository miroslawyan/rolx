import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { PhaseEditPageComponent } from '@app/projects/pages/phase-edit-page/phase-edit-page.component';
import { ProjectDetailPageComponent } from '@app/projects/pages/project-detail-page/project-detail-page.component';
import { ProjectEditPageComponent } from '@app/projects/pages/project-edit-page/project-edit-page.component';
import { ProjectListPageComponent } from '@app/projects/pages/project-list-page/project-list-page.component';
import { PhaseFormComponent } from '@app/projects/shared/phase/form/phase-form.component';
import { PhaseSelectorComponent } from '@app/projects/shared/phase/selector/phase-selector.component';
import { PhaseTableComponent } from '@app/projects/shared/phase/table/phase-table.component';
import { ProjectFormComponent } from '@app/projects/shared/project/form/project-form.component';
import { ProjectTableComponent } from '@app/projects/shared/project/table/project-table.component';
import { StarredPhaseIndicatorComponent } from '@app/projects/shared/starred-phase-indicator/starred-phase-indicator.component';

@NgModule({
  imports: [AppImportModule, CommonModule],
  declarations: [
    PhaseEditPageComponent,
    ProjectDetailPageComponent,
    ProjectEditPageComponent,
    ProjectListPageComponent,
    PhaseFormComponent,
    PhaseSelectorComponent,
    PhaseTableComponent,
    ProjectFormComponent,
    ProjectTableComponent,
    StarredPhaseIndicatorComponent,
  ],
  exports: [
    PhaseEditPageComponent,
    ProjectDetailPageComponent,
    ProjectEditPageComponent,
    ProjectListPageComponent,
    PhaseSelectorComponent,
    StarredPhaseIndicatorComponent,
  ],
})
export class ProjectsModule {}
