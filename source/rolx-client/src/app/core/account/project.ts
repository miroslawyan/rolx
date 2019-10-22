import moment from 'moment';

import { DataWrapper } from '@app/core/util';
import { Customer } from './customer';

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

  private customerShadow: Customer;
  private openUntilShadow: moment.Moment;

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

    this.customerShadow = this.raw.customerId ? {
      id: this.raw.customerId,
      number: this.raw.customerNumber,
      name: this.raw.customerName,
    } : undefined;

    this.openUntilShadow = this.raw.openUntil ? moment(this.raw.openUntil) : null;
  }

  get number() { return this.raw.number; }
  set number(value: string) { this.raw.number = value; }

  get name() { return this.raw.name; }
  set name(value) { this.raw.name = value; }

  get customer() {
    return this.customerShadow;
  }

  set customer(value: Customer) {
    this.customerShadow = value;
    this.raw.customerId = value ? value.id : undefined;
    this.raw.customerNumber = value ? value.number : '';
    this.raw.customerName = value ? value.name : '';
  }

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
