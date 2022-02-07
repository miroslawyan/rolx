import { Duration, TransformAsDuration } from '@app/core/util/duration';
import { TransformAsIsoDate } from '@app/core/util/iso-date';
import { assertDefined } from '@app/core/util/utils';
import * as moment from 'moment';

export class Balance {
  @TransformAsIsoDate()
  byDate!: moment.Moment;

  @TransformAsDuration()
  overtime!: Duration;

  vacationAvailableDays!: number;
  vacationPlannedDays!: number;

  validateModel(): void {
    assertDefined(this, 'byDate');
    assertDefined(this, 'overtime');
    assertDefined(this, 'vacationAvailableDays');
    assertDefined(this, 'vacationPlannedDays');
  }
}
