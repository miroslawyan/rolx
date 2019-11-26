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
    PhaseTableComponent,
    ProjectFormComponent,
    ProjectTableComponent,
  ],
  exports: [
    PhaseEditPageComponent,
    ProjectDetailPageComponent,
    ProjectEditPageComponent,
    ProjectListPageComponent,
  ],
})
export class AccountModule { }
