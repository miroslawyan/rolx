import { Phase } from '@app/account/core';
import { Duration, TransformAsDuration, TransformAsIsoDate } from '@app/core/util';
import { Type } from 'class-transformer';
import moment from 'moment';
import { DayType } from './day-type';
import { RecordEntry } from './record-entry';

export class Record {

  id: number;

  @TransformAsIsoDate()
  date: moment.Moment;

  userId: string;
  dayType: DayType = DayType.Workday;
  dayName = '';

  @TransformAsDuration()
  nominalWorkTime = Duration.Zero;

  @Type(() => RecordEntry)
  entries: RecordEntry[] = [];

  get totalDuration(): Duration {
    return new Duration(
      this.entries.reduce(
        (sum, e) => sum + e.duration.seconds, 0));
  }

  entriesOf(phase: Phase): RecordEntry[] {
    return this.entries.filter(e => e.phase.id === phase.id);
  }

}
