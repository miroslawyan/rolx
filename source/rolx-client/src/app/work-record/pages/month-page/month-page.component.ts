import { Component, OnInit } from '@angular/core';
import { WorkRecordService } from '@app/work-record/core';
import moment from 'moment';
import { BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-month-page',
  templateUrl: './month-page.component.html',
  styleUrls: ['./month-page.component.scss'],
})
export class MonthPageComponent implements OnInit {

  month$ = new BehaviorSubject<moment.Moment>(moment());
  records$ = this.month$.pipe(
    switchMap(m => this.workRecordService.getMonth(m)),
  );

  constructor(private workRecordService: WorkRecordService) {}

  ngOnInit() {
  }

  previous() {
    this.month$.next(moment(this.month$.value).subtract(1, 'month'));
  }

  next() {
    this.month$.next(moment(this.month$.value).add(1, 'month'));
  }

}
