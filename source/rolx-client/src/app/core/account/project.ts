import moment from 'moment';

import { DataWrapper } from '@app/core/util';

export interface ProjectData {
  id: number;
  number: string;
  name: string;
  customerId: number;
  customerName: string;
  customerNumber: string;
  openUntil: string | null;
}

export class Project extends DataWrapper<ProjectData> {

  private openUntilShadow: moment.Moment = null;

  constructor(data?: ProjectData) {
    super(data ? data : {
      id: undefined,
      number: '',
      name: '',
      customerId: undefined,
      customerName: '',
      customerNumber: '',
      openUntil: null,
    });

    if (this.raw.openUntil != null) {
      this.openUntilShadow = moment(this.raw.openUntil);
    }
  }

  get number() { return this.raw.number; }
  set number(value: string) { this.raw.number = value; }

  get name() { return this.raw.name; }
  set name(value) { this.raw.name = value; }

  get customerName() { return this.raw.customerName; }
  set customerName(value) { this.raw.customerName = value; }

  get openUntil(): moment.Moment | null {
    return this.openUntilShadow;
  }

  set openUntil(value: moment.Moment | null) {
    this.openUntilShadow = value;
    this.raw.openUntil = value != null ? value.toISOString(true) : null;
  }

  get isOpen(): boolean {
    return this.openUntil == null || moment().isBefore(this.openUntil, 'day');
  }

}
