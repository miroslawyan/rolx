import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ErrorResponse } from '@app/core/error/error-response';
import { IsoDate } from '@app/core/util/iso-date';
import { mapPlainToInstances } from '@app/core/util/operators';
import { environment } from '@env/environment';
import { instanceToPlain } from 'class-transformer';
import * as moment from 'moment';
import { Observable, ReplaySubject, Subject, throwError } from 'rxjs';
import { catchError, mapTo, switchMap, tap } from 'rxjs/operators';

import { Record } from './record';

@Injectable({
  providedIn: 'root',
})
export class WorkRecordService {
  private static readonly Url = environment.apiBaseUrl + '/v1/workrecord';

  private readonly userUpdatedSubject = new Subject<string>();
  private updateSequence = new ReplaySubject<number>(1);

  userUpdated$ = this.userUpdatedSubject.asObservable();

  constructor(private httpClient: HttpClient) {
    console.log('--- WorkRecordService.ctor()');

    this.updateSequence.next(1);
  }

  getRange(userId: string, begin: moment.Moment, end: moment.Moment): Observable<Record[]> {
    const url =
      WorkRecordService.UrlWithId(userId) +
      '/range/' +
      IsoDate.fromMoment(begin) +
      '..' +
      IsoDate.fromMoment(end);

    return this.httpClient.get<any[]>(url).pipe(
      mapPlainToInstances(Record),
      tap((rs) => rs.forEach((r) => r.validateModel())),
    );
  }

  update(userId: string, record: Record): Observable<Record> {
    const currentSequence = this.updateSequence;
    const nextSequence = new ReplaySubject<number>(1);
    this.updateSequence = nextSequence;

    return currentSequence.pipe(
      switchMap(() => this.internalUpdate(userId, record)),
      tap(() => nextSequence.next(0)),
      catchError((e) => {
        nextSequence.next(0);
        return throwError(() => e);
      }),
    );
  }

  private static UrlWithId(userId: string): string {
    return WorkRecordService.Url + '/' + userId;
  }

  private internalUpdate(userId: string, record: Record): Observable<Record> {
    const url = WorkRecordService.UrlWithId(userId) + '/' + IsoDate.fromMoment(record.date);
    return this.httpClient.put(url, instanceToPlain(record)).pipe(
      mapTo(record),
      tap((r) => this.userUpdatedSubject.next(r.userId)),
      catchError((e) => throwError(() => new ErrorResponse(e))),
    );
  }
}
