import { Duration, TransformAsDuration } from '@app/core/util/duration';
import { TransformAsIsoDate } from '@app/core/util/iso-date';
import { assertDefined } from '@app/core/util/utils';
import * as moment from 'moment';

export class Phase {
  id!: number;
  number!: number;
  name!: string;
  fullName!: string;

  @TransformAsIsoDate()
  startDate!: moment.Moment;

  @TransformAsIsoDate()
  endDate?: moment.Moment;

  isBillable!: boolean;

  @TransformAsDuration()
  budget!: Duration;

  validateModel(): void {
    assertDefined(this, 'id');
    assertDefined(this, 'number');
    assertDefined(this, 'name');
    assertDefined(this, 'fullName');
    assertDefined(this, 'startDate');
    assertDefined(this, 'isBillable');
    assertDefined(this, 'budget');
  }

  isOpenAt(date: moment.Moment): boolean {
    return date >= this.startDate && (this.endDate == null || date <= this.endDate);
  }
}
