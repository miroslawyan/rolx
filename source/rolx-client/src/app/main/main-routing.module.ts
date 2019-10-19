import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard, AuthResolve } from '@app/core/auth';
import { CustomerFormComponent } from './customer-form/customer-form.component';
import { CustomerTableComponent } from './customer-table/customer-table.component';
import { MainPageComponent } from './main-page/main-page.component';
import { MonthPageComponent } from './month-page/month-page.component';
import { ProjectTableComponent } from './project-table/project-table.component';
import { SignInPageComponent } from './sign-in-page/sign-in-page.component';

const routes: Routes = [
    {
      path: '',
      component: MainPageComponent,
      canActivate: [AuthGuard],
      children: [
        {
          path: '',
          component: MonthPageComponent,
        },
        {
          path: 'customer/:id',
          component: CustomerFormComponent,
        },
        {
          path: 'customer',
          component: CustomerTableComponent,
        },
        {
          path: 'project',
          component: ProjectTableComponent,
        },
      ],
    },
    {
      path: 'sign-in',
      component: SignInPageComponent,
      resolve: { items: AuthResolve },
    },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
