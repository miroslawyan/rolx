import { Transform } from 'class-transformer';
import moment from 'moment';

export class IsoDateTime {

  static toMoment(isoDate: string | null): moment.Moment | null {
    return isoDate != null ? moment(isoDate) : null;
  }

  static fromMoment(date: moment.Moment | null): string | null {
    return moment.isMoment(date) ? date.toISOString() : null;
  }

}

export function TransformAsIsoDateTime(): (target: any, key: string) => void {
  const toClass = Transform(v => IsoDateTime.toMoment(v), { toClassOnly: true });
  const toPlain = Transform(v => IsoDateTime.fromMoment(v), { toPlainOnly: true });

  return (target: any, key: string) => {
    toClass(target, key);
    toPlain(target, key);
  };
}
