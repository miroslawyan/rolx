import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard, AuthResolve } from '@app/core/auth';
import { SetupResolve } from '@app/core/setup';
import { NotFoundPageComponent } from '@app/main/not-found-page/not-found-page.component';
import { ProjectEditPageComponent } from '@app/main/project-edit-page/project-edit-page.component';
import { CustomerEditPageComponent } from './customer-edit-page/customer-edit-page.component';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { MonthPageComponent } from './month-page/month-page.component';
import { ProjectListPageComponent } from './project-list-page/project-list-page.component';
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
          path: 'customer/:id',
          component: CustomerEditPageComponent,
        },
        {
          path: 'customer',
          component: CustomerListPageComponent,
        },
        {
          path: 'project/:id',
          component: ProjectEditPageComponent,
        },
        {
          path: 'project',
          component: ProjectListPageComponent,
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
