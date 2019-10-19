import { ProjectData } from '@app/core/account';
import dayjs from 'dayjs';

import { DataWrapper } from '@app/core/util';
import { DayType } from './day-type';

export interface RecordData {
  date: Date;
  dayType: DayType;
  dayName: string;
  nominalWorkTimeHours: number;
}

export class Record extends DataWrapper<RecordData> {

  readonly date = dayjs(this.raw.date);

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
