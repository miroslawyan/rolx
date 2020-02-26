import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AuthInterceptor } from '@app/auth/core';
import { PendingRequestInterceptor } from './pending-request';
import { EnumToArrayPipe } from './util';

@NgModule({
  imports: [
    CommonModule,
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: PendingRequestInterceptor, multi: true},
  ],
  declarations: [
    EnumToArrayPipe,
  ],
  exports: [
    EnumToArrayPipe,
  ],
})
export class CoreModule { }
