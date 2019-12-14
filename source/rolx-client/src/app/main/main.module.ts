import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AccountModule} from '@app/account';
import { AppImportModule } from '@app/app-import.module';
import { WorkRecordModule } from '@app/work-record';
import { MainPageComponent } from './main-page/main-page.component';
import { MainRoutingModule } from './main-routing.module';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { SignInPageComponent } from './sign-in-page/sign-in-page.component';

@NgModule({
  declarations: [
    MainPageComponent,
    SignInPageComponent,
    NotFoundPageComponent,
  ],
  imports: [
    AccountModule,
    AppImportModule,
    CommonModule,
    MainRoutingModule,
    WorkRecordModule,
  ],
})
export class MainModule { }
