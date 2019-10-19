import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { AuthenticatedUserData } from '@app/core/auth/authenticated-user.data';
import { SignInData } from '@app/core/auth/sign-in.data';
import { environment } from '@env/environment';
import { Info } from './info';

const SignInUrl = environment.apiBaseUrl + '/v1/signin';

@Injectable({
  providedIn: 'root'
})
export class SignInService {
  constructor(private httpClient: HttpClient) {
    console.log('--- SignInService.ctor()');
  }

  getInfo(): Observable<Info> {
    return this.httpClient.get<Info>(SignInUrl + '/info');
  }

  signIn(signInData: SignInData): Observable<AuthenticatedUserData> {
    return this.httpClient.post<AuthenticatedUserData>(SignInUrl, signInData);
  }
}
