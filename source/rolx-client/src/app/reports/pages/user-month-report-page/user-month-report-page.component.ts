import { Component } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { UserMonthReport } from '@app/reports/core/user-month-report';
import { UserReportService } from '@app/reports/core/user-report.service';
import * as moment from 'moment';
import { Observable, switchMap } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'rolx-user-month-report-page',
  templateUrl: './user-month-report-page.component.html',
  styleUrls: ['./user-month-report-page.component.scss'],
})
export class UserMonthReportPageComponent {
  readonly month$: Observable<moment.Moment> = this.route.paramMap.pipe(
    map((params) => this.parseRouteMonth(params)),
  );

  readonly report$: Observable<UserMonthReport> = this.month$.pipe(
    switchMap((month) => this.userReportService.getMonthReport(month)),
  );

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userReportService: UserReportService,
  ) {}

  async previous(month: moment.Moment): Promise<void> {
    await this.navigateTo(month.clone().subtract(1, 'month'));
  }

  async next(month: moment.Moment): Promise<void> {
    await this.navigateTo(month.clone().add(1, 'month'));
  }

  private async navigateTo(date: moment.Moment): Promise<void> {
    await this.router.navigate(['reports', 'user', date.year(), date.month() + 1]);
  }

  private parseRouteMonth(params: ParamMap): moment.Moment {
    const year = parseInt(params.get('year') ?? '', 10) || null;
    if (year == null) {
      return moment();
    }

    const month = parseInt(params.get('month') ?? '', 10) || 1;
    if (month < 1 || month > 12) {
      // noinspection JSIgnoredPromiseFromCall
      this.router.navigate(['four-oh-four']);
      return moment();
    }

    return moment({ year, month: month - 1, day: 1 });
  }
}
