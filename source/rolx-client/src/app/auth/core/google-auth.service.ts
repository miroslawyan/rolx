import { ElementRef, Injectable, NgZone } from '@angular/core';
import { CredentialResponse } from 'google-one-tap';
import { lastValueFrom, Subject } from 'rxjs';

import { Info } from './info';
import { SignInService } from './sign-in.service';

declare global {
  const google: typeof import('google-one-tap');
}

@Injectable({
  providedIn: 'root',
})
export class GoogleAuthService {
  private readonly credentialsSubject = new Subject<string>();
  private isInitialized = false;

  $credentials = this.credentialsSubject.asObservable();

  constructor(private signInService: SignInService, private zone: NgZone) {
    console.log('--- GoogleAuthService.ctor()');
  }

  async initialize(): Promise<void> {
    // we initialize at most once
    if (this.isInitialized) {
      return;
    }

    console.log('--- GoogleAuthService.initialize()');
    const info = await this.loadSignInInfo();

    google.accounts.id.initialize({
      client_id: info.googleClientId,
      callback: (r) => this.propagateCredentials(r),
    });

    this.isInitialized = true;
    console.log('--- GoogleAuthService.initialize() done');
  }

  renderButton(button: ElementRef) {
    google.accounts.id.renderButton(
      button.nativeElement,
      { theme: 'outline', size: 'large' }, // customization attributes
    );

    google.accounts.id.prompt(); // also display the One Tap dialog
  }

  private propagateCredentials(credentialResponse: CredentialResponse) {
    this.zone.run(() => this.credentialsSubject.next(credentialResponse.credential));
  }

  private loadSignInInfo(): Promise<Info> {
    return lastValueFrom(this.signInService.getInfo());
  }
}
