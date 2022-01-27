import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AuthInterceptor } from '@app/auth/core/auth.interceptor';
import { PendingRequestInterceptor } from '@app/core/pending-request/pending-request.interceptor';
import { EnumToArrayPipe } from '@app/core/util/enum-to-array.pipe';

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
