import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { environment } from '@env/environment';
import { Info } from './info';

const SetupUrl = environment.apiBaseUrl + '/v1/setup';

@Injectable({
  providedIn: 'root'
})
export class SetupService {

  info: Info;

  constructor(private httpClient: HttpClient) { }

  initialize(): Observable<void> {
    return this.httpClient.get<Info>(SetupUrl).pipe(
      tap(i => this.info = i),
      map(() => void 0)
    );
  }

}
