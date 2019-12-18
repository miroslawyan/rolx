import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IsoDate, mapPlainToClassArray } from '@app/core/util';
import { environment } from '@env/environment';
import moment from 'moment';
import { Observable } from 'rxjs';
import { Phase } from './phase';

const PhaseUrl = environment.apiBaseUrl + '/v1/phase';

@Injectable({
  providedIn: 'root',
})
export class PhaseService {

  constructor(private httpClient: HttpClient) { }

  getAll(unlessEndedBefore: moment.Moment = null): Observable<Phase[]> {
    const params = unlessEndedBefore ? {
        unlessEndedBeforeDate: IsoDate.fromMoment(unlessEndedBefore),
    } : null;

    return this.httpClient.get<object[]>(PhaseUrl, { params }).pipe(
      mapPlainToClassArray(Phase),
    );
  }

  getSuitable(date: moment.Moment): Observable<Phase[]> {
    const url = `${PhaseUrl}/suitable/${IsoDate.fromMoment(date)}`;

    return this.httpClient.get<object[]>(url).pipe(
      mapPlainToClassArray(Phase),
    );
  }
}
