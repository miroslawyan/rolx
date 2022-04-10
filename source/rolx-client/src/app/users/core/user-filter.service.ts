import { Injectable } from '@angular/core';
import { Role } from '@app/users/core/role';
import { User } from '@app/users/core/user';

@Injectable({
  providedIn: 'root',
})
export class UserFilterService {
  private _filterText = '';

  showLefties = false;

  get filterText() {
    return this._filterText;
  }
  set filterText(value: string) {
    this._filterText = value.trim();
  }

  static Predicate(user: User, filter: string): boolean {
    return `${user.fullName},${Role[user.role]}`.toLowerCase().includes(filter);
  }
}
