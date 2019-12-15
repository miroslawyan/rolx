import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import {
  PhaseEditPageComponent,
  ProjectDetailPageComponent,
  ProjectEditPageComponent,
  ProjectListPageComponent,
} from './pages';
import {
  PhaseFormComponent,
  PhaseSelectorComponent,
  PhaseTableComponent,
  ProjectFormComponent,
  ProjectTableComponent,
  StarredPhaseIndicatorComponent,
} from './shared';

@NgModule({
  imports: [
    AppImportModule,
    CommonModule,
  ],
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
export class AccountModule { }
