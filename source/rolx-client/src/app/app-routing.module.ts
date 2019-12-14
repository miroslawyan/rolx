import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainModule } from './main';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => MainModule,
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      routes,
      // { enableTracing: true },
      ),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule { }
