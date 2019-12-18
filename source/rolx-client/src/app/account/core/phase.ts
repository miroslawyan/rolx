import { Duration, TransformAsDuration, TransformAsIsoDate } from '@app/core/util';
import moment from 'moment';

export class Phase {
  id: number;
  number: number;
  name: string;
  fullName: string;

  @TransformAsIsoDate()
  startDate: moment.Moment;

  @TransformAsIsoDate()
  endDate: moment.Moment;

  isBillable: boolean;

  @TransformAsDuration()
  budget: Duration;

  isOpenAt(date: moment.Moment): boolean {
    return date >= this.startDate &&
      (this.endDate == null || date <= this.endDate);
  }
}
