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
  entryDate: moment.Moment;

  get fullName(): string {
    return `${this.firstName} ${this.lastName}`;
  }
}
