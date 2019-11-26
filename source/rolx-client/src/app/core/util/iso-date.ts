import moment from 'moment';

export class IsoDate {

  static toMoment(isoDate: string | null): moment.Moment | null {
    return isoDate != null ? moment(isoDate) : null;
  }

  static fromMoment(date: moment.Moment | null): string | null {
    return moment.isMoment(date) ? date.format('YYYY-MM-DD') : null;
  }

}
