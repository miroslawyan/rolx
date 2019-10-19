import dayjs from 'dayjs';

import { DataWrapper } from '@app/core/util';

export interface ProjectData {
  id: number;
  number: string;
  name: string;
  customerId: number;
  customerName: string;
  customerNumber: string;
  openUntil: Date | null;
}

export class Project extends DataWrapper<ProjectData> {

  get number() { return this.raw.number; }
  set number(value: string) { this.raw.number = value; }

  get name() { return this.raw.name; }
  set name(value) { this.raw.name = value; }

  get customerName() { return this.raw.customerName; }
  set customerName(value) { this.raw.customerName = value; }

  get openUntil(): dayjs.Dayjs | null {
    return this.raw.openUntil != null ? dayjs(this.raw.openUntil) : null;
  }

  set openUntil(value: dayjs.Dayjs | null) {
    this.raw.openUntil = value != null ? value.toDate() : null;
  }

  get isOpen(): boolean {
    return this.raw.openUntil == null || dayjs().isBefore(this.openUntil, 'day');
  }

}
