import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ImportModule } from '@app/import';
import { SignInPageComponent } from '@app/main/sign-in-page/sign-in-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { MainRoutingModule } from './main-routing.module';
import { MonthPageComponent } from './month-page/month-page.component';
import { MonthTableComponent } from './month-page/month-table/month-table.component';

@NgModule({
  declarations: [
    MainPageComponent,
    SignInPageComponent,
    MonthPageComponent,
    MonthTableComponent,
  ],
  imports: [
    CommonModule,
    ImportModule,
    MainRoutingModule,
  ]
})
export class MainModule { }
