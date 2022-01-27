import { TransformAsIsoDateTime } from '@app/core/util/iso-date-time';
import { User } from '@app/users/core/user';
import { Type } from 'class-transformer';
import moment from 'moment';

export class Approval {

  @Type(() => User)
  user: User;

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
