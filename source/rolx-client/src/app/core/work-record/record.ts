import dayjs from 'dayjs';

import { DataWrapper } from '@app/core/util';

export interface RecordData {
  date: Date;
  name: string;
}

export class Record extends DataWrapper<RecordData> {

  date = dayjs(this.raw.date);

  get name() {
    return this.raw.name;
  }

}
