import { Role } from '@app/auth/core';
import { TransformAsIsoDate } from '@app/core/util';
import { Exclude } from 'class-transformer';
import moment from 'moment';

export class User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  avatarUrl: string;
  role: Role;

  @TransformAsIsoDate()
  entryDate: moment.Moment | null;

  @TransformAsIsoDate()
  leftDate: moment.Moment | null;

  @Exclude()
  get leavingDate(): moment.Moment | null {
    return this.leftDate?.clone()?.subtract(1, 'days') ?? null;
  }
  set leavingDate(value) {
    this.leftDate = value?.clone()?.add(1, 'days') ?? null;
  }

  @Exclude()
  get fullName(): string {
    return `${this.firstName} ${this.lastName}`;
  }

  @Exclude()
  isActiveAt(date: moment.Moment): boolean {
    return this.entryDate != null
      && this.entryDate.isSameOrBefore(date, 'day')
      && (this.leftDate == null || this.leftDate.isAfter(date, 'day'));
  }
}
