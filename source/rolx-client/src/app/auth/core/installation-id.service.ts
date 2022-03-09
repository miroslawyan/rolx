import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { SignInService } from '@app/auth/core/sign-in.service';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class InstallationIdService {
  private readonly suffixSubject = new BehaviorSubject<string>(' - Offline');

  readonly suffix$ = this.suffixSubject.asObservable();
  get suffix() {
    return this.suffixSubject.value;
  }

  constructor(signInService: SignInService, titleService: Title) {
    signInService
      .getInfo()
      .pipe(
        map((info) => (info.installationId !== 'Production' ? ' - ' + info.installationId : '')),
      )
      .subscribe((suffix) => {
        this.suffixSubject.next(suffix);
        titleService.setTitle('RolX' + suffix);
      });
  }
}
