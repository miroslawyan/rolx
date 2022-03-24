import { ParamMap } from '@angular/router';
import * as moment from 'moment';

export class UserMonthReportParams {
  private readonly nextMonth = this.month.clone().add(1, 'month');
  private readonly previousMonth = this.month.clone().subtract(1, 'month');

  public readonly nextRouteCommands = ['/reports', this.nextMonth.format('YYYY-MM')];
  public readonly previousRouteCommands = ['/reports', this.previousMonth.format('YYYY-MM')];
  public readonly queryParams = this.isCurrentUser ? {} : { userId: this.userId };

  constructor(
    public readonly month: moment.Moment,
    public readonly userId: string,
    public readonly isCurrentUser: boolean,
  ) {}

  public static evaluate(
    params: ParamMap,
    queryParamMap: ParamMap,
    currentUserId: string,
  ): UserMonthReportParams {
    const userId = queryParamMap.get('userId') ?? currentUserId;
    const isCurrentUser = userId === currentUserId;

    const month = moment(params.get('month') || {});
    if (!month.isValid()) {
      throw new Error(`failed to parse date: ${params.get('month')}`);
    }

    return new UserMonthReportParams(month.startOf('month'), userId, isCurrentUser);
  }
}
