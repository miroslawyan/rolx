import moment from 'moment';

import { DataWrapper } from '@app/core/util';
import { Customer } from './customer';

export interface ProjectData {
  id: number;
  number: string;
  name: string;
  customer: Customer | null;
  openUntilDate: string | null;
}

export class Project extends DataWrapper<ProjectData> {

  private openUntilShadow: moment.Moment;

  constructor(data?: ProjectData) {
    super(data ? data : {
      id: undefined,
      number: '',
      name: '',
      customer: null,
      openUntilDate: null,
    });

    this.openUntilShadow = this.raw.openUntilDate ? moment(this.raw.openUntilDate) : null;
  }

  get number() { return this.raw.number; }
  set number(value: string) { this.raw.number = value; }

  get name() { return this.raw.name; }
  set name(value) { this.raw.name = value; }

  get customer() { return this.raw.customer; }
  set customer(value: Customer) { this.raw.customer = value; }

  get customerName() { return this.raw.customer.name; }

  get openUntil(): moment.Moment | null {
    return this.openUntilShadow;
  }

  set openUntil(value: moment.Moment | null) {
    this.openUntilShadow = value;
    this.raw.openUntilDate = value != null ? value.format('YYYY-MM-DD') : null;
  }

  get isOpen(): boolean {
    return this.openUntil == null || moment().isBefore(this.openUntil, 'day');
  }

}
