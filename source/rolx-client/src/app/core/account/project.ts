import moment from 'moment';
import { Customer } from './customer';

export interface ProjectData {
  id: number;
  number: string;
  name: string;
  customer: Customer | null;
  openUntilDate: string | null;
}

export class Project {
  id: number;
  number: string;
  name: string;
  customer: Customer | null;
  openUntil: moment.Moment;

  static fromData(data: ProjectData): Project {
    const instance = new Project();

    Object.assign(instance, data);
    instance.openUntil = data.openUntilDate ? moment(data.openUntilDate) : null;

    return instance;
  }

  toData(): ProjectData {
    const data = {} as ProjectData;

    Object.assign(data, this);
    data.openUntilDate = moment.isMoment(this.openUntil) ? this.openUntil.format('YYYY-MM-DD') : null;

    return data;
  }

  get customerName() { return this.customer.name; }

  get isOpen(): boolean {
    return this.openUntil == null || moment().isBefore(this.openUntil, 'day');
  }
}
