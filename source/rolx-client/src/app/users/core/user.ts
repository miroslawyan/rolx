import { TransformAsIsoDate } from '@app/core/util/iso-date';
import { assertDefined } from '@app/core/util/utils';
import { Exclude } from 'class-transformer';
import * as moment from 'moment';

import { Role } from './role';

export class User {
  id!: string;
  firstName!: string;
  lastName!: string;
  email!: string;
  avatarUrl!: string;
  role!: Role;

  @TransformAsIsoDate()
  entryDate!: moment.Moment;

  @TransformAsIsoDate()
  leavingDate?: moment.Moment;

  isConfirmed!: boolean;

  validateModel(): void {
    assertDefined(this, 'id');
    assertDefined(this, 'firstName');
    assertDefined(this, 'lastName');
    assertDefined(this, 'email');
    assertDefined(this, 'avatarUrl');
    assertDefined(this, 'role');
    assertDefined(this, 'entryDate');
    assertDefined(this, 'isConfirmed');
  }

  @Exclude()
  get fullName(): string {
    return `${this.firstName} ${this.lastName}`;
  }

  @Exclude()
  isActiveAt(date: moment.Moment): boolean {
    return (
      this.entryDate.isSameOrBefore(date, 'day') &&
      (this.leavingDate == null || this.leavingDate.isSameOrAfter(date, 'day'))
    );
  }
}
