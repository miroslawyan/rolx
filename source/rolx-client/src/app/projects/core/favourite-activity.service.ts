import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { mapPlainToInstances } from '@app/core/util/operators';
import { ActivityService } from '@app/projects/core/activity.service';
import { instanceToPlain } from 'class-transformer';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { mapTo, switchMap, tap } from 'rxjs/operators';

import { Activity } from './activity';

@Injectable({
  providedIn: 'root',
})
export class FavouriteActivityService {
  private static readonly Url = ActivityService.Url + '/favourite';

  private favouritesSubject = new BehaviorSubject<Activity[]>([]);
  private favouriteIdsSubject = new BehaviorSubject<Set<number>>(new Set<number>());

  favourites$ = this.favouritesSubject.asObservable();
  favouriteIds$ = this.favouriteIdsSubject.asObservable();

  constructor(private httpClient: HttpClient) {
    this.refresh().subscribe();
  }

  isFavourite(activity: Activity): boolean {
    return this.favouriteIdsSubject.value.has(activity.id);
  }

  getAll(): Observable<Activity[]> {
    return this.httpClient
      .get<object[]>(FavouriteActivityService.Url)
      .pipe(mapPlainToInstances(Activity));
  }

  add(activity: Activity): Observable<Activity> {
    if (this.isFavourite(activity)) {
      return of(activity);
    }

    return this.httpClient.put(FavouriteActivityService.Url, instanceToPlain(activity)).pipe(
      switchMap(() => this.refresh()),
      mapTo(activity),
    );
  }

  remove(activity: Activity): Observable<Activity> {
    if (!this.isFavourite(activity)) {
      return of(activity);
    }

    return this.httpClient
      .request('delete', FavouriteActivityService.Url, { body: instanceToPlain(activity) })
      .pipe(
        switchMap(() => this.refresh()),
        mapTo(activity),
      );
  }

  private refresh(): Observable<Activity[]> {
    return this.getAll().pipe(
      tap((as) => this.favouriteIdsSubject.next(new Set<number>(as.map((a) => a.id)))),
      tap((as) => this.favouritesSubject.next(as)),
    );
  }
}
