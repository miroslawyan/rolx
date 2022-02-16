import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IsoDate } from '@app/core/util/iso-date';
import { mapPlainToInstances } from '@app/core/util/operators';
import { environment } from '@env/environment';
import * as moment from 'moment';
import { Observable, tap } from 'rxjs';

import { Activity } from './activity';

@Injectable({
  providedIn: 'root',
})
export class ActivityService {
  public static readonly Url = environment.apiBaseUrl + '/v1/activity';

  constructor(private httpClient: HttpClient) {}

  getAll(unlessEndedBefore: moment.Moment | null = null): Observable<Activity[]> {
    const isoUnlessEndedBefore = IsoDate.fromMoment(unlessEndedBefore);
    const options = isoUnlessEndedBefore
      ? {
          params: {
            unlessEndedBeforeDate: isoUnlessEndedBefore,
          },
        }
      : undefined;

    return this.httpClient.get<object[]>(ActivityService.Url, options).pipe(
      mapPlainToInstances(Activity),
      tap((as) => as.forEach((a) => a.validateModel())),
    );
  }

  getSuitable(date: moment.Moment): Observable<Activity[]> {
    const url = `${ActivityService.Url}/suitable/${IsoDate.fromMoment(date)}`;

    return this.httpClient.get<object[]>(url).pipe(
      mapPlainToInstances(Activity),
      tap((as) => as.forEach((a) => a.validateModel())),
    );
  }
}
