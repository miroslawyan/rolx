import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ImportModule } from '@app/import';
import { SignInPageComponent } from '@app/main/sign-in-page/sign-in-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { MainRoutingModule } from './main-routing.module';

@NgModule({
  declarations: [
    MainPageComponent,
    SignInPageComponent,
  ],
  imports: [
    CommonModule,
    ImportModule,
    MainRoutingModule,
  ]
})
export class MainModule { }
