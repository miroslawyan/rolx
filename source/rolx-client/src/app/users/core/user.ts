import { Role } from '@app/auth/core/role';
import { TransformAsIsoDate } from '@app/core/util/iso-date';
import { assertDefined } from '@app/core/util/utils';
import { Exclude } from 'class-transformer';
import * as moment from 'moment';

export class User {
  id!: string;
  firstName!: string;
  lastName!: string;
  email!: string;
  avatarUrl!: string;
  role!: Role;

  @TransformAsIsoDate()
  entryDate?: moment.Moment;

  @TransformAsIsoDate()
  leftDate?: moment.Moment;

  validateModel(): void {
    assertDefined(this, 'id');
    assertDefined(this, 'firstName');
    assertDefined(this, 'lastName');
    assertDefined(this, 'email');
    assertDefined(this, 'avatarUrl');
    assertDefined(this, 'role');
  }

  @Exclude()
  get leavingDate(): moment.Moment | undefined {
    return this.leftDate?.clone()?.subtract(1, 'days');
  }
  set leavingDate(value) {
    this.leftDate = value?.clone()?.add(1, 'days');
  }

  @Exclude()
  get fullName(): string {
    return `${this.firstName} ${this.lastName}`;
  }

  @Exclude()
  isActiveAt(date: moment.Moment): boolean {
    return (
      this.entryDate != null &&
      this.entryDate.isSameOrBefore(date, 'day') &&
      (this.leftDate == null || this.leftDate.isAfter(date, 'day'))
    );
  }
}
