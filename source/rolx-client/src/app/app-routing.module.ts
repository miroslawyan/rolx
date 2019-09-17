import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MainComponent } from './main/main.component';
import { SignInComponent } from './sign-in/sign-in.component';


const routes: Routes = [
  { path: '', component: MainComponent },
  { path: 'sign-in', component: SignInComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
