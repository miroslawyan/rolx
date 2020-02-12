import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AccountModule} from '@app/account';
import { AppImportModule } from '@app/app-import.module';
import { AuthModule } from '@app/auth';
import { UsersModule } from '@app/users';
import { WorkRecordModule } from '@app/work-record';
import { ForbiddenPageComponent } from './forbidden-page/forbidden-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { MainRoutingModule } from './main-routing.module';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { SignInPageComponent } from './sign-in-page/sign-in-page.component';

@NgModule({
  declarations: [
    ForbiddenPageComponent,
    MainPageComponent,
    NotFoundPageComponent,
    SignInPageComponent,
  ],
  imports: [
    AccountModule,
    AppImportModule,
    AuthModule,
    CommonModule,
    MainRoutingModule,
    UsersModule,
    WorkRecordModule,
  ],
})
export class MainModule { }
