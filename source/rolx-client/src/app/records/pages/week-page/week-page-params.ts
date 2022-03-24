import { ParamMap } from '@angular/router';
import { IsoDate } from '@app/core/util/iso-date';
import * as moment from 'moment';

export class WeekPageParams {
  private readonly previousMonday = this.monday.clone().subtract(1, 'week');

  public readonly nextSunday = this.monday.clone().add(6, 'days');
  public readonly nextMonday = this.monday.clone().add(1, 'week');

  public readonly nextRouteCommands = ['/week', IsoDate.fromMoment(this.nextMonday)];
  public readonly previousRouteCommands = ['/week', IsoDate.fromMoment(this.previousMonday)];
  public readonly queryParams = this.isCurrentUser ? {} : { userId: this.userId };

  constructor(
    public readonly monday: moment.Moment,
    public readonly userId: string,
    public readonly isCurrentUser: boolean,
  ) {}

  public static evaluate(
    params: ParamMap,
    queryParamMap: ParamMap,
    currentUserId: string,
  ): WeekPageParams {
    const userId = queryParamMap.get('userId') ?? currentUserId;
    const isCurrentUser = userId === currentUserId;

    const date = moment(params.get('date') || {});
    if (!date.isValid()) {
      throw new Error(`failed to parse date: ${params.get('date')}`);
    }

    return new WeekPageParams(date.isoWeekday(1), userId, isCurrentUser);
  }
}
