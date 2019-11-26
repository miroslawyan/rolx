import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AccountModule} from '@app/account';
import { AppImportModule } from '@app/app-import.module';
import { SharedModule } from '@app/shared';
import { MainPageComponent } from './main-page/main-page.component';
import { MainRoutingModule } from './main-routing.module';
import { MonthPageComponent } from './month-page/month-page.component';
import { MonthTableComponent } from './month-page/month-table/month-table.component';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { SignInPageComponent } from './sign-in-page/sign-in-page.component';

@NgModule({
  declarations: [
    MainPageComponent,
    MonthPageComponent,
    MonthTableComponent,
    SignInPageComponent,
    NotFoundPageComponent,
  ],
  imports: [
    AccountModule,
    AppImportModule,
    CommonModule,
    MainRoutingModule,
    SharedModule,
  ],
})
export class MainModule { }
