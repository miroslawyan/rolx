import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Role } from '@app/auth/core';
import { UserData } from '@app/auth/core/user.data';
import { mapPlainToClassArray } from '@app/core/util';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';

const UserUrl = environment.apiBaseUrl + '/v1/user';

export class User implements UserData {
  id: string;  googleId: string;
  firstName: string;
  lastName: string;
  email: string;
  avatarUrl: string;
  role: Role;

  get fullName(): string {
    return `${this.firstName} ${this.lastName}`;
  }
}

@Injectable({
  providedIn: 'root',
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  getAll(): Observable<User[]> {
    return this.httpClient.get(UserUrl).pipe(
      mapPlainToClassArray(User),
    );
  }
}
