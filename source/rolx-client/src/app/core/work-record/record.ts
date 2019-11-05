import { DataWrapper } from '@app/core/util';
import moment from 'moment';
import { DayType } from './day-type';

export interface RecordData {
  date: string;
  dayType: DayType;
  dayName: string;
  nominalWorkTimeHours: number;
}

export class Record extends DataWrapper<RecordData> {

  readonly date = moment(this.raw.date);

  get dayType() {
    return this.raw.dayType;
  }

  get dayName() {
    return this.raw.dayName;
  }

  get nominalWorkTimeHours() {
    return this.raw.nominalWorkTimeHours;
  }

}
