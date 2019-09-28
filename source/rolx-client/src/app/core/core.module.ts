import { CommonModule } from '@angular/common';
import { APP_INITIALIZER, NgModule } from '@angular/core';

import { AuthService } from '@app/core/auth';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    { provide: APP_INITIALIZER, useFactory: AuthService.Initializer, deps: [AuthService], multi: true }
  ],
})
export class CoreModule { }
