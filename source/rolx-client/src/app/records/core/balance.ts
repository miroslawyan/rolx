import { Duration, TransformAsDuration } from '@app/core/util/duration';
import { TransformAsIsoDate } from '@app/core/util/iso-date';
import moment from 'moment';

export class Balance {

  @TransformAsIsoDate()
  byDate: moment.Moment;

  @TransformAsDuration()
  overtime: Duration;

  vacationAvailableDays: number;
  vacationPlannedDays: number;

}
