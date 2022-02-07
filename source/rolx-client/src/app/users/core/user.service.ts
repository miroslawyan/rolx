import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ErrorResponse } from '@app/core/error/error-response';
import { mapPlainToInstance, mapPlainToInstances } from '@app/core/util/operators';
import { environment } from '@env/environment';
import { instanceToPlain } from 'class-transformer';
import { Observable, throwError } from 'rxjs';
import { catchError, mapTo, tap } from 'rxjs/operators';

import { User } from './user';

const UserUrl = environment.apiBaseUrl + '/v1/user';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<User[]> {
    return this.httpClient.get<object[]>(UserUrl).pipe(
      mapPlainToInstances(User),
      tap((us) => us.forEach((u) => u.validateModel())),
    );
  }

  getById(id: string): Observable<User> {
    return this.httpClient.get(UserService.UrlWithId(id)).pipe(
      mapPlainToInstance(User),
      tap((u) => u.validateModel()),
    );
  }

  update(user: User): Observable<User> {
    return this.httpClient.put(UserService.UrlWithId(user.id), instanceToPlain(user)).pipe(
      mapTo(user),
      catchError((e) => throwError(new ErrorResponse(e))),
    );
  }

  private static UrlWithId(id: string) {
    return UserUrl + '/' + id;
  }
}
