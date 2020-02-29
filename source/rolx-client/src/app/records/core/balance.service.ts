import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IsoDate, mapPlainToClass } from '@app/core/util';
import { environment } from '@env/environment';
import moment from 'moment';
import { Observable } from 'rxjs';
import { Balance } from './balance';

const BalanceUrl = environment.apiBaseUrl + '/v1/balance';

@Injectable({
  providedIn: 'root',
})
export class BalanceService {

  constructor(private httpClient: HttpClient) {
  }

  getByDate(date: moment.Moment): Observable<Balance> {
    const url = BalanceUrl + '/' + IsoDate.fromMoment(date);

    return this.httpClient.get(url).pipe(
      mapPlainToClass(Balance),
    );
  }

}
