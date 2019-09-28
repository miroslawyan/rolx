import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '@env/environment';
import { Info } from './info';

const SignInUrl = environment.apiBaseUrl + '/v1/signin';

@Injectable({
  providedIn: 'root'
})
export class SignInService {
  constructor(private httpClient: HttpClient) { }

  getInfo(): Observable<Info> {
    return this.httpClient.get<Info>(SignInUrl + '/info');
  }
}
