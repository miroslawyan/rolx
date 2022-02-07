import { Transform } from 'class-transformer';
import * as moment from 'moment';

export class IsoDate {
  static toMoment(isoDate: string | null): moment.Moment | null {
    return isoDate != null ? moment(isoDate) : null;
  }

  static fromMoment(date: moment.Moment | null): string | null {
    return moment.isMoment(date) ? date.format('YYYY-MM-DD') : null;
  }
}

export const TransformAsIsoDate = (): ((target: any, key: string) => void) => {
  const toClass = Transform(({ value }) => IsoDate.toMoment(value), { toClassOnly: true });
  const toPlain = Transform(({ value }) => IsoDate.fromMoment(value), { toPlainOnly: true });

  return (target: any, key: string) => {
    toClass(target, key);
    toPlain(target, key);
  };
};
