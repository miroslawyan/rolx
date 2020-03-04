import { Duration, TransformAsDuration, TransformAsIsoDate } from '@app/core/util';
import moment from 'moment';

export class Balance {

  @TransformAsIsoDate()
  byDate: moment.Moment;

  @TransformAsDuration()
  overtime: Duration;

  vacationAvailableDays: number;
  vacationPlannedDays: number;

}
