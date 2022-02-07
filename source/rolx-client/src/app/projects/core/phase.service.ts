import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IsoDate } from '@app/core/util/iso-date';
import { mapPlainToInstances } from '@app/core/util/operators';
import { environment } from '@env/environment';
import * as moment from 'moment';
import { Observable, tap } from 'rxjs';

import { Phase } from './phase';

const PhaseUrl = environment.apiBaseUrl + '/v1/phase';

@Injectable({
  providedIn: 'root',
})
export class PhaseService {
  constructor(private httpClient: HttpClient) {}

  getAll(unlessEndedBefore: moment.Moment | null = null): Observable<Phase[]> {
    const isoUnlessEndedBefore = IsoDate.fromMoment(unlessEndedBefore);
    const options = isoUnlessEndedBefore
      ? {
          params: {
            unlessEndedBeforeDate: isoUnlessEndedBefore,
          },
        }
      : undefined;

    return this.httpClient.get<object[]>(PhaseUrl, options).pipe(
      mapPlainToInstances(Phase),
      tap((ps) => ps.forEach((p) => p.validateModel())),
    );
  }

  getSuitable(date: moment.Moment): Observable<Phase[]> {
    const url = `${PhaseUrl}/suitable/${IsoDate.fromMoment(date)}`;

    return this.httpClient.get<object[]>(url).pipe(
      mapPlainToInstances(Phase),
      tap((ps) => ps.forEach((p) => p.validateModel())),
    );
  }
}
