import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { ProjectsModule} from '@app/projects';
import { RecordsModule } from '@app/records';
import { UsersModule } from '@app/users';
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
    ProjectsModule,
    AppImportModule,
    CommonModule,
    MainRoutingModule,
    UsersModule,
    RecordsModule,
  ],
})
export class MainModule { }
