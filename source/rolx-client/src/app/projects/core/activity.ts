import { Duration, TransformAsDuration } from '@app/core/util/duration';
import { TransformAsIsoDate } from '@app/core/util/iso-date';
import { assertDefined } from '@app/core/util/utils';
import { Billability } from '@app/projects/core/billability';
import { Type } from 'class-transformer';
import * as moment from 'moment';

export class Activity {
  id!: number;
  number!: number;
  name!: string;

  @TransformAsIsoDate()
  startDate!: moment.Moment;

  @TransformAsIsoDate()
  endDate?: moment.Moment;

  @Type(() => Billability)
  billability!: Billability;

  @TransformAsDuration()
  budget!: Duration;

  fullNumber!: string;
  fullName!: string;

  validateModel(): void {
    assertDefined(this, 'id');
    assertDefined(this, 'number');
    assertDefined(this, 'name');
    assertDefined(this, 'startDate');
    assertDefined(this, 'billability');
    assertDefined(this, 'budget');
    assertDefined(this, 'fullNumber');
    assertDefined(this, 'fullName');

    this.billability.validateModel();
  }

  isOpenAt(date: moment.Moment): boolean {
    return date >= this.startDate && (this.endDate == null || date <= this.endDate);
  }

  get isBillable(): boolean {
    return this.billability.isBillable;
  }
}
