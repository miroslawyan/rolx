import { Duration, TransformAsDuration } from '@app/core/util/duration';
import { TransformAsIsoDate } from '@app/core/util/iso-date';
import { assertDefined } from '@app/core/util/utils';
import * as moment from 'moment';

export class MonthlyWorkTime {
  @TransformAsIsoDate()
  month!: moment.Moment;

  days!: number;

  @TransformAsDuration()
  hours!: Duration;

  validateModel(): void {
    assertDefined(this, 'month');
    assertDefined(this, 'days');
    assertDefined(this, 'hours');
  }
}
