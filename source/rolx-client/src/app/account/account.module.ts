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
  ],
  exports: [
    PhaseEditPageComponent,
    ProjectDetailPageComponent,
    ProjectEditPageComponent,
    ProjectListPageComponent,
    PhaseSelectorComponent,
  ],
})
export class AccountModule { }
