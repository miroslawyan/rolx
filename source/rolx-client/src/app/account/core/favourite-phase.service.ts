import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Phase } from '@app/account/core/phase';
import { mapPlainToClassArray } from '@app/core/util';
import { environment } from '@env/environment';
import { classToPlain } from 'class-transformer';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { mapTo, switchMap, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class FavouritePhaseService {

  private static readonly Url = environment.apiBaseUrl + '/v1/phase/favourite';

  private favouritesSubject = new BehaviorSubject<Phase[]>([]);
  private favouriteIdsSubject = new BehaviorSubject<Set<number>>(new Set<number>());

  favourites$ = this.favouritesSubject.asObservable();
  favouriteIds$ = this.favouriteIdsSubject.asObservable();

  constructor(private httpClient: HttpClient) {
    this.refresh().subscribe();
  }

  isFavourite(phase: Phase): boolean {
    return this.favouriteIdsSubject.value.has(phase.id);
  }

  getAll(): Observable<Phase[]> {
    return this.httpClient.get<object[]>(FavouritePhaseService.Url).pipe(
      mapPlainToClassArray(Phase),
    );
  }

  add(phase: Phase): Observable<Phase> {
    if (this.isFavourite(phase)) {
      return of(phase);
    }

    return this.httpClient.put(FavouritePhaseService.Url, classToPlain(phase)).pipe(
      switchMap(() => this.refresh()),
      mapTo(phase),
    );
  }

  remove(phase: Phase): Observable<Phase> {
    if (!this.isFavourite(phase)) {
      return of(phase);
    }

    return this.httpClient.request('delete', FavouritePhaseService.Url, { body: classToPlain(phase)}).pipe(
      switchMap(() => this.refresh()),
      mapTo(phase),
    );
  }

  private refresh(): Observable<Phase[]> {
    return this.getAll().pipe(
      tap(phs => this.favouriteIdsSubject.next(new Set<number>(phs.map(ph => ph.id)))),
      tap(phs => this.favouritesSubject.next(phs)),
    );
  }
}
