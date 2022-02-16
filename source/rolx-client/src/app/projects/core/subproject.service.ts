import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ErrorResponse } from '@app/core/error/error-response';
import { mapPlainToInstance, mapPlainToInstances } from '@app/core/util/operators';
import { environment } from '@env/environment';
import { instanceToPlain } from 'class-transformer';
import { Observable, tap, throwError } from 'rxjs';
import { catchError, mapTo } from 'rxjs/operators';

import { Subproject } from './subproject';

@Injectable({
  providedIn: 'root',
})
export class SubprojectService {
  private static readonly Url = environment.apiBaseUrl + '/v1/subproject';

  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<Subproject[]> {
    return this.httpClient.get<object[]>(SubprojectService.Url).pipe(
      mapPlainToInstances(Subproject),
      tap((ps) => ps.forEach((p) => p.validateModel())),
    );
  }

  getById(id: number): Observable<Subproject> {
    return this.httpClient.get(SubprojectService.UrlWithId(id)).pipe(
      mapPlainToInstance(Subproject),
      tap((p) => p.validateModel()),
    );
  }

  create(subproject: Subproject): Observable<Subproject> {
    return this.httpClient.post(SubprojectService.Url, instanceToPlain(subproject)).pipe(
      mapPlainToInstance(Subproject),
      tap((p) => p.validateModel()),
      catchError((e) => throwError(() => new ErrorResponse(e))),
    );
  }

  update(subproject: Subproject): Observable<Subproject> {
    return this.httpClient
      .put(SubprojectService.UrlWithId(subproject.id), instanceToPlain(subproject))
      .pipe(
        mapTo(subproject),
        catchError((e) => throwError(() => new ErrorResponse(e))),
      );
  }

  private static UrlWithId(id: number) {
    return SubprojectService.Url + '/' + id;
  }
}
