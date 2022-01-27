import { Duration, TransformAsDuration } from '@app/core/util/duration';
import { TransformAsIsoDate } from '@app/core/util/iso-date';
import moment from 'moment';

export class MonthlyWorkTime {
  @TransformAsIsoDate()
  month: moment.Moment;

  days: number;

  @TransformAsDuration()
  hours: Duration;
}
