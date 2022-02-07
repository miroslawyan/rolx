import { Transform } from 'class-transformer';
import * as moment from 'moment';

export class IsoDateTime {
  static toMoment(isoDate: string | null): moment.Moment | null {
    return isoDate != null ? moment(isoDate) : null;
  }

  static fromMoment(date: moment.Moment | null): string | null {
    return moment.isMoment(date) ? date.toISOString() : null;
  }
}

export const TransformAsIsoDateTime = (): ((target: any, key: string) => void) => {
  const toClass = Transform(({ value }) => IsoDateTime.toMoment(value), { toClassOnly: true });
  const toPlain = Transform(({ value }) => IsoDateTime.fromMoment(value), { toPlainOnly: true });

  return (target: any, key: string) => {
    toClass(target, key);
    toPlain(target, key);
  };
};
