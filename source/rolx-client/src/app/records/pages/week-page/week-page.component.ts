import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@app/auth/core/auth.service';
import { IsoDate } from '@app/core/util/iso-date';
import { Phase } from '@app/projects/core/phase';
import { PhaseService } from '@app/projects/core/phase.service';
import { Record } from '@app/records/core/record';
import { WorkRecordService } from '@app/records/core/work-record.service';
import moment from 'moment';
import { forkJoin, Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-week-page',
  templateUrl: './week-page.component.html',
})
export class WeekPageComponent implements OnInit {

  monday$: Observable<moment.Moment>;
  recordsAndPhases$: Observable<[Record[], Phase[]]>;
  user = this.authService.currentApproval?.user;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private workRecordService: WorkRecordService,
              private phaseService: PhaseService,
              private authService: AuthService) { }

  ngOnInit() {
    this.monday$ = this.route.paramMap.pipe(
      map(params => moment(params.get('date') ?? {})),
      map(date => date.clone().isoWeekday(1)),
    );

    this.recordsAndPhases$ = this.monday$.pipe(
      switchMap(monday => forkJoin([
        this.workRecordService.getRange(monday, monday.clone().add(7, 'days')),
        this.phaseService.getSuitable(monday),
      ])),
    );
  }

  previous(monday: moment.Moment) {
    this.navigateTo(monday.clone().subtract(1, 'week'));
  }

  next(monday: moment.Moment) {
    this.navigateTo(monday.clone().add(1, 'week'));
  }

  private navigateTo(date: moment.Moment) {
    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(['week', IsoDate.fromMoment(date)]);
  }

}
