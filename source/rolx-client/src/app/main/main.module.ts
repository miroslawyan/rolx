import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { ProjectsModule } from '@app/projects/projects.module';
import { RecordsModule } from '@app/records/records.module';
import { UsersModule } from '@app/users/users.module';
import { ForbiddenPageComponent } from './forbidden-page/forbidden-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { MainRoutingModule } from './main-routing.module';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { SignInPageComponent } from './sign-in-page/sign-in-page.component';
import { SomethingWentWrongPageComponent } from './something-went-wrong-page/something-went-wrong-page.component';
import { ToolbarComponent } from './toolbar/toolbar.component';

@NgModule({
  declarations: [
    ForbiddenPageComponent,
    MainPageComponent,
    NotFoundPageComponent,
    SignInPageComponent,
    SomethingWentWrongPageComponent,
    ToolbarComponent,
  ],
  imports: [
    AppImportModule,
    CommonModule,
    MainRoutingModule,
    ProjectsModule,
    RecordsModule,
    UsersModule,
  ],
})
export class MainModule { }
