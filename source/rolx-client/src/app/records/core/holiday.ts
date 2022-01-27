import { TransformAsIsoDate } from '@app/core/util/iso-date';
import moment from 'moment';

export class Holiday {
  @TransformAsIsoDate()
  date: moment.Moment;
  name: string;
}
