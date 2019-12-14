import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IsoDate } from '@app/core/util';
import { Record, WorkRecordService } from '@app/work-record/core';
import moment from 'moment';
import { Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-week-page',
  templateUrl: './week-page.component.html',
  styleUrls: ['./week-page.component.scss'],
})
export class WeekPageComponent implements OnInit {

  monday$: Observable<moment.Moment>;
  records$: Observable<Record[]>;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private workRecordService: WorkRecordService) { }

  ngOnInit() {
    this.monday$ = this.route.paramMap.pipe(
      map(params => moment(params.get('date'))),
      map(date => date.isValid() ? date : moment()),
      map(date => date.clone().isoWeekday(1)),
    );

    this.records$ = this.monday$.pipe(
      switchMap(monday => this.workRecordService.getRange(monday, monday.clone().add(7, 'days'))),
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
