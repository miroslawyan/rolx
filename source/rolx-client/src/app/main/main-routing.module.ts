import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard, AuthResolve } from '@app/core/auth';
import { MainPageComponent } from './main-page/main-page.component';
import { SignInPageComponent } from './sign-in-page/sign-in-page.component';

const routes: Routes = [
    {
      path: '',
      component: MainPageComponent,
      canActivate: [AuthGuard],
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
