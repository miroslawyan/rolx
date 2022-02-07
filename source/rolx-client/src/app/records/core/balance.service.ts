import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IsoDate } from '@app/core/util/iso-date';
import { mapPlainToInstance } from '@app/core/util/operators';
import { environment } from '@env/environment';
import * as moment from 'moment';
import { Observable, tap } from 'rxjs';

import { Balance } from './balance';

const BalanceUrl = environment.apiBaseUrl + '/v1/balance';

@Injectable({
  providedIn: 'root',
})
export class BalanceService {
  constructor(private httpClient: HttpClient) {}

  getByDate(date: moment.Moment): Observable<Balance> {
    const url = BalanceUrl + '/' + IsoDate.fromMoment(date);

    return this.httpClient.get(url).pipe(
      mapPlainToInstance(Balance),
      tap((b) => b.validateModel()),
    );
  }
}
