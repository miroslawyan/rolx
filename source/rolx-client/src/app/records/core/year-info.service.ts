import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { mapPlainToClass } from '@app/core/util/operators';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';

import { YearInfo } from './year-info';

const YearInfoUrl = environment.apiBaseUrl + '/v1/yearinfo';

@Injectable({
  providedIn: 'root',
})

export class YearInfoService {

  constructor(private httpClient: HttpClient) {
  }

  getYearInfo(year: number): Observable<YearInfo> {
    const url = YearInfoUrl + '/' + year;

    return this.httpClient.get(url).pipe(
      mapPlainToClass(YearInfo),
    );
    }
}
