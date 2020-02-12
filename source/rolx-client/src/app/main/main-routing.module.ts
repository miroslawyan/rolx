import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountRoutes } from '@app/account';
import { AuthGuard, AuthResolve } from '@app/auth/core';
import { SetupResolve } from '@app/core/setup';
import { UsersRoutes } from '@app/users';
import { WorkRecordRoutes } from '@app/work-record';
import { ForbiddenPageComponent } from './forbidden-page/forbidden-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { SignInPageComponent } from './sign-in-page/sign-in-page.component';

const routes: Routes = [
  {
    path: '',
    component: MainPageComponent,
    canActivate: [AuthGuard],
    resolve: { items: SetupResolve },
    children: [
      ...AccountRoutes,
      ...UsersRoutes,
      ...WorkRecordRoutes,
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
    path: 'forbidden',
    component: ForbiddenPageComponent,
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
