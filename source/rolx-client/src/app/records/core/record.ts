import { Duration, TransformAsDuration, TransformAsIsoDate } from '@app/core/util';
import { Phase } from '@app/projects/core';
import { Type } from 'class-transformer';
import moment from 'moment';
import { DayType } from './day-type';
import { RecordEntry } from './record-entry';

export class Record {

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

  get overtime(): Duration {
    return this.totalDuration.sub(this.nominalWorkTime);
  }

  get isWorkday(): boolean {
    return this.dayType === DayType.Workday;
  }

  entriesOf(phase: Phase): RecordEntry[] {
    return this.entries.filter(e => e.phaseId === phase.id);
  }

  replaceEntriesOfPhase(phase: Phase, entries: RecordEntry[]): Record {
    const clone = new Record();
    Object.assign(clone, this);

    entries = entries.filter(e => !e.duration.isZero);
    entries.forEach(e => e.phaseId = phase.id);

    clone.entries = this.entries
      .filter(e => e.phaseId !== phase.id)
      .concat(...entries);

    return clone;
  }

}
