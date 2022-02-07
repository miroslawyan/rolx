import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { mapPlainToInstance } from '@app/core/util/operators';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { Approval } from './approval';
import { Info } from './info';
import { SignInData } from './sign-in.data';

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

  signIn(signInData: SignInData): Observable<Approval> {
    return this.httpClient.post(SignInUrl, signInData).pipe(
      mapPlainToInstance(Approval),
      tap((a) => a.validateModel()),
    );
  }

  extend(): Observable<Approval> {
    return this.httpClient.get(SignInUrl + '/extend').pipe(
      mapPlainToInstance(Approval),
      tap((a) => a.validateModel()),
    );
  }
}
