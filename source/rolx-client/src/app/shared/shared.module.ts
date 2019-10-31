import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { AppImportModule } from '@app/app-import.module';
import { CustomerFormComponent, CustomerTableComponent } from './customer';
import { ProjectFormComponent, ProjectTableComponent } from './project';

@NgModule({
  imports: [
    CommonModule,
    AppImportModule,
  ],
  exports: [
    CustomerFormComponent,
    CustomerTableComponent,
    ProjectFormComponent,
    ProjectTableComponent,
  ],
  declarations: [
    CustomerFormComponent,
    CustomerTableComponent,
    ProjectFormComponent,
    ProjectTableComponent,
  ],
})
export class SharedModule { }
