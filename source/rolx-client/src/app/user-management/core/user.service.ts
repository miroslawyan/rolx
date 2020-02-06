import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ErrorResponse } from '@app/core/error';
import { mapPlainToClass, mapPlainToClassArray } from '@app/core/util';
import { environment } from '@env/environment';
import { classToPlain } from 'class-transformer';
import { Observable, throwError } from 'rxjs';
import { catchError, mapTo } from 'rxjs/operators';
import { User } from './user';

const UserUrl = environment.apiBaseUrl + '/v1/user';

@Injectable({
  providedIn: 'root',
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  private static UrlWithId(id: string) {
    return UserUrl + '/' + id;
  }

  getAll(): Observable<User[]> {
    return this.httpClient.get(UserUrl).pipe(
      mapPlainToClassArray(User),
    );
  }

  getById(id: string): Observable<User> {
    return this.httpClient.get(UserService.UrlWithId(id)).pipe(
      mapPlainToClass(User),
    );
  }

  update(user: User): Observable<User> {
    return this.httpClient.put(UserService.UrlWithId(user.id), classToPlain(user)).pipe(
      mapTo(user),
      catchError(e => throwError(new ErrorResponse(e))),
    );
  }
}
