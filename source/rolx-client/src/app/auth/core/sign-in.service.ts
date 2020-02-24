import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticatedUser } from '@app/auth/core/authenticated.user';
import { SignInData } from '@app/auth/core/sign-in.data';
import { mapPlainToClass } from '@app/core/util';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { Info } from './info';

const SignInUrl = environment.apiBaseUrl + '/v1/signin';

@Injectable({
  providedIn: 'root',
})
export class SignInService {
  constructor(private httpClient: HttpClient) {
    console.log('--- SignInService.ctor()');
  }

  getInfo(): Observable<Info> {
    return this.httpClient.get<Info>(SignInUrl + '/info');
  }

  signIn(signInData: SignInData): Observable<AuthenticatedUser> {
    return this.httpClient.post(SignInUrl, signInData).pipe(
      mapPlainToClass(AuthenticatedUser),
    );
  }

  extend(): Observable<AuthenticatedUser> {
    return this.httpClient.get(SignInUrl + '/extend').pipe(
      mapPlainToClass(AuthenticatedUser),
    );
  }
}
