import { Role } from '@app/auth/core';
import { TransformAsIsoDate } from '@app/core/util';
import moment from 'moment';

export class User {
  id: string;
  googleId: string;
  firstName: string;
  lastName: string;
  email: string;
  avatarUrl: string;
  role: Role;

  @TransformAsIsoDate()
  entryDate: moment.Moment | null;

  @TransformAsIsoDate()
  leavingDate: moment.Moment | null;

  get fullName(): string {
    return `${this.firstName} ${this.lastName}`;
  }

  isActiveAt(date: moment.Moment): boolean {
    return this.entryDate != null
      && this.entryDate.isSameOrBefore(date, 'day')
      && (this.leavingDate == null || this.leavingDate.isSameOrAfter(date, 'day'));
  }
}
