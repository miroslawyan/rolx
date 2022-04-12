import { Duration, TransformAsDuration } from '@app/core/util/duration';
import { TransformAsIsoDate } from '@app/core/util/iso-date';
import { assertDefined } from '@app/core/util/utils';
import { Exclude } from 'class-transformer';
import * as moment from 'moment';

export class Activity {
  id!: number;
  number!: number;
  name!: string;

  @TransformAsIsoDate()
  startDate!: moment.Moment;

  @TransformAsIsoDate()
  endedDate?: moment.Moment;

  billabilityId!: number;
  billabilityName!: string;
  isBillable!: boolean;

  @TransformAsDuration()
  budget!: Duration;

  @TransformAsDuration()
  actual!: Duration;

  fullNumber!: string;
  fullName!: string;

  validateModel(): void {
    assertDefined(this, 'id');
    assertDefined(this, 'number');
    assertDefined(this, 'name');
    assertDefined(this, 'startDate');
    assertDefined(this, 'billabilityId');
    assertDefined(this, 'billabilityName');
    assertDefined(this, 'isBillable');
    assertDefined(this, 'budget');
    assertDefined(this, 'actual');
    assertDefined(this, 'fullNumber');
    assertDefined(this, 'fullName');
  }

  @Exclude()
  get endDate(): moment.Moment | undefined {
    return this.endedDate?.clone()?.subtract(1, 'days');
  }
  set endDate(value) {
    this.endedDate = value?.clone()?.add(1, 'days');
  }

  isOpenAt(date: moment.Moment): boolean {
    return date >= this.startDate && (this.endedDate == null || date < this.endedDate);
  }
}
