import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { mapPlainToInstance } from '@app/core/util/operators';
import { EditLock } from '@app/records/core/edit-lock';
import { environment } from '@env/environment';
import { instanceToPlain } from 'class-transformer';
import * as moment from 'moment';
import { BehaviorSubject, lastValueFrom, Observable, of, Subject, switchMap } from 'rxjs';
import { catchError, distinctUntilChanged, map, tap, throttleTime } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class EditLockService {
  private static readonly Url = environment.apiBaseUrl + '/v1/edit-lock';

  private readonly refreshSubject = new Subject<void>();
  private readonly dateSubject = new BehaviorSubject<moment.Moment>(moment().startOf('day'));

  readonly date$: Observable<moment.Moment> = this.dateSubject.asObservable();
  get date() {
    return this.dateSubject.value;
  }

  constructor(private readonly httpClient: HttpClient) {
    console.log('--- EditLockService.ctor()');
    this.refreshSubject
      .pipe(
        throttleTime(60 * 1000),
        tap(() => console.log('--- EditLockService refresh')),
        switchMap(() => this.get()),
        map((l) => l.date),
        catchError(() => of(this.dateSubject.value)),
        distinctUntilChanged((d1, d2) => d1.isSame(d2, 'day')),
      )
      .subscribe((d) => this.dateSubject.next(d));

    this.refresh();
  }

  async set(value: moment.Moment): Promise<void> {
    const data = new EditLock();
    data.date = value;

    await lastValueFrom(this.httpClient.put(EditLockService.Url, instanceToPlain(data)));
    this.dateSubject.next(value);
  }

  refresh(): void {
    this.refreshSubject.next();
  }

  isLocked(date: moment.Moment): boolean {
    return this.date.isAfter(date, 'days');
  }

  private get(): Observable<EditLock> {
    return this.httpClient.get(EditLockService.Url).pipe(
      mapPlainToInstance(EditLock),
      tap((l) => l.validateModel()),
    );
  }
}
