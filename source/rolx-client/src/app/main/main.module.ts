import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { AppImportModule } from '@app/app-import.module';
import { SharedModule } from '@app/shared';
import { CustomerEditPageComponent } from './customer-edit-page/customer-edit-page.component';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { MainRoutingModule } from './main-routing.module';
import { MonthPageComponent } from './month-page/month-page.component';
import { MonthTableComponent } from './month-page/month-table/month-table.component';
import { ProjectEditPageComponent } from './project-edit-page/project-edit-page.component';
import { ProjectListPageComponent } from './project-list-page/project-list-page.component';
import { SignInPageComponent } from './sign-in-page/sign-in-page.component';

@NgModule({
  declarations: [
    CustomerEditPageComponent,
    CustomerListPageComponent,
    MainPageComponent,
    MonthPageComponent,
    MonthTableComponent,
    ProjectEditPageComponent,
    ProjectListPageComponent,
    SignInPageComponent,
  ],
  imports: [
    AppImportModule,
    CommonModule,
    MainRoutingModule,
    SharedModule,
  ]
})
export class MainModule { }
