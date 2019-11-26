import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
  PhaseEditPageComponent,
  ProjectDetailPageComponent,
  ProjectEditPageComponent,
  ProjectListPageComponent,
} from '@app/account/pages';
import { AuthGuard, AuthResolve } from '@app/core/auth';
import { SetupResolve } from '@app/core/setup';
import { MainPageComponent } from './main-page/main-page.component';
import { MonthPageComponent } from './month-page/month-page.component';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { SignInPageComponent } from './sign-in-page/sign-in-page.component';

const routes: Routes = [
    {
      path: '',
      component: MainPageComponent,
      canActivate: [AuthGuard],
      resolve: { items: SetupResolve },
      children: [
        {
          path: '',
          component: MonthPageComponent,
        },
        {
          path: 'project',
          component: ProjectListPageComponent,
        },
        {
          path: 'project/:id',
          component: ProjectDetailPageComponent,
        },
        {
          path: 'project/:id/edit',
          component: ProjectEditPageComponent,
        },
        {
          path: 'project/:id/phase/:phaseId',
          component: PhaseEditPageComponent,
        },
        {
          path: 'four-oh-four',
          component: NotFoundPageComponent,
        },
      ],
    },
    {
      path: 'sign-in',
      component: SignInPageComponent,
      resolve: { items: AuthResolve },
    },
    {
      path: '**',
      redirectTo: '/four-oh-four',
    },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainRoutingModule { }
