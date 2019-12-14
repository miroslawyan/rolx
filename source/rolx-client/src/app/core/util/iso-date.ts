import { Transform } from 'class-transformer';
import moment from 'moment';

export class IsoDate {

  static toMoment(isoDate: string | null): moment.Moment | null {
    return isoDate != null ? moment(isoDate) : null;
  }

  static fromMoment(date: moment.Moment | null): string | null {
    return moment.isMoment(date) ? date.format('YYYY-MM-DD') : null;
  }

}

export function TransformAsIsoDate(): (target: any, key: string) => void {
  const toClass = Transform(v => IsoDate.toMoment(v), { toClassOnly: true });
  const toPlain = Transform(v => IsoDate.fromMoment(v), { toPlainOnly: true });

  return (target: any, key: string) => {
    toClass(target, key);
    toPlain(target, key);
  };
}
