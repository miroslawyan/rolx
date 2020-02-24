import { Injectable } from '@angular/core';
import { Info } from './info';
import { SignInService } from './sign-in.service';

@Injectable({
  providedIn: 'root',
})
export class GoogleAuthService {
  private auth2: gapi.auth2.GoogleAuth | null = null;

  constructor(private signInService: SignInService) {
    console.log('--- GoogleAuthService.ctor()');
  }

  async initialize(): Promise<void> {
    // we initialize at most once
    if (this.auth2) {
      return;
    }

    console.log('--- GoogleAuthService.initialize()');
    const [, info] = await Promise.all([
      await this.loadGApi(),
      await this.loadSignInInfo(),
    ]);

    this.auth2 = gapi.auth2.init({
      client_id: info.googleClientId,
    });

    // ensure auth2 is initialized
    await this.auth2.then(() => true, e => console.error('initializing gapi.auth2 failed:', e));

    console.log('--- GoogleAuthService.initialize() done');
  }

  async signIn(): Promise<gapi.auth2.GoogleUser> {
    if (this.auth2 == null) {
      throw new Error('initialize() must complete before performing any further requests');
    }

    return await this.auth2.signIn();
  }

  signOut() {
    if (this.auth2 == null) {
      throw new Error('initialize() must complete before performing any further requests');
    }

    this.auth2.signOut();
  }

  private loadGApi(): Promise<void> {
    return new Promise<void>(resolve =>
      gapi.load('auth2', () => resolve()));
  }

  private loadSignInInfo(): Promise<Info> {
    return this.signInService.getInfo().toPromise();
  }
}
