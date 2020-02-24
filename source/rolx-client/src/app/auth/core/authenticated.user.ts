import { TransformAsIsoDateTime } from '@app/core/util';
import { User } from '@app/users/core';
import moment from 'moment';

export class AuthenticatedUser extends User {
  bearerToken: string;

  @TransformAsIsoDateTime()
  expires: moment.Moment;

  get isExpired(): boolean {
    return this.expires.isBefore(moment().add(5, 'm'));
  }

  get willExpireSoon(): boolean {
    return this.expires.isBefore(moment().add(15, 'm'));
  }
}
