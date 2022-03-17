import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@app/auth/core/auth.service';
import { IsoDate } from '@app/core/util/iso-date';
import { ActivityService } from '@app/projects/core/activity.service';
import { WorkRecordService } from '@app/records/core/work-record.service';
import * as moment from 'moment';
import { forkJoin, Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-week-page',
  templateUrl: './week-page.component.html',
})
export class WeekPageComponent {
  readonly monday$: Observable<moment.Moment> = this.route.paramMap.pipe(
    map((params) => moment(params.get('date') ?? {})),
    map((date) => date.clone().isoWeekday(1)),
  );

  readonly recordsAndActivities$ = this.monday$.pipe(
    switchMap((monday) =>
      forkJoin([
        this.workRecordService.getRange(monday, monday.clone().add(7, 'days')),
        this.activityService.getSuitable(monday),
      ]),
    ),
  );

  readonly user = this.authService.currentApproval?.user;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private workRecordService: WorkRecordService,
    private activityService: ActivityService,
    private authService: AuthService,
  ) {}

  async previous(monday: moment.Moment): Promise<void> {
    await this.navigateTo(monday.clone().subtract(1, 'week'));
  }

  async next(monday: moment.Moment): Promise<void> {
    await this.navigateTo(monday.clone().add(1, 'week'));
  }

  private async navigateTo(date: moment.Moment): Promise<void> {
    await this.router.navigate(['week', IsoDate.fromMoment(date)]);
  }
}
