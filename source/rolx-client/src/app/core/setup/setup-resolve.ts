import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { SetupService } from './setup.service';

@Injectable({
  providedIn: 'root',
})
export class SetupResolve implements Resolve<void> {

  constructor(private setupService: SetupService) {
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.setupService.initialize();
  }

}
