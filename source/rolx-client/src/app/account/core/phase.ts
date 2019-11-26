import { IsoDate } from '@app/core/util';
import moment from 'moment';

export class Phase {
  id: number;
  number: number;
  name: string;
  fullName: string;
  startDate: moment.Moment | null;
  endDate: moment.Moment | null;
  isBillable: boolean;
  budgetHours: number | null;

  static fromJson(data: any): Phase {
    const instance = new Phase();

    Object.assign(instance, data);

    instance.startDate = IsoDate.toMoment(data.startDate);
    instance.endDate = IsoDate.toMoment(data.endDate);

    return instance;
  }

  toJson(): any {
    const data = {
      startDate: undefined,
      endDate: undefined,
      budgetHours: undefined,
    };

    Object.assign(data, this);

    data.startDate = IsoDate.fromMoment(this.startDate);
    data.endDate = IsoDate.fromMoment(this.endDate);
    data.budgetHours = this.budgetHours > 0.1 ? this.budgetHours : null;

    return data;
  }
}
