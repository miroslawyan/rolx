import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, of, timer } from 'rxjs';
import {
  delay,
  distinctUntilChanged,
  filter,
  map,
  shareReplay,
  startWith,
  switchMap,
} from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class PendingRequestService {
  private readonly openRequests$ = new BehaviorSubject(0);

  readonly hasOverdueRequest$: Observable<boolean>;

  constructor() {
    const busy$ = this.openRequests$.pipe(
      map((v) => v !== 0),
      distinctUntilChanged(),
      switchMap((b) => (b ? of(true).pipe(delay(200)) : of(false))),
    );

    const minOverdue$ = busy$.pipe(
      filter((b) => b),
      switchMap(() =>
        timer(500).pipe(
          map(() => false),
          startWith(true),
        ),
      ),
      startWith(false),
    );

    this.hasOverdueRequest$ = combineLatest([busy$, minOverdue$]).pipe(
      map(([a, b]) => a || b),
      distinctUntilChanged(),
      shareReplay(1),
    );
  }

  requestStarted() {
    this.openRequests$.next(this.openRequests$.value + 1);
  }

  requestFinished() {
    const next = this.openRequests$.value - 1;

    if (next < 0) {
      console.warn('number of pending requests < 0', next);
    }

    this.openRequests$.next(Math.max(0, next));
  }
}
