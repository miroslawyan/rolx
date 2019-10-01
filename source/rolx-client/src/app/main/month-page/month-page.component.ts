import { Component, OnInit } from '@angular/core';
import dayjs, { Dayjs } from 'dayjs';
import { BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { WorkRecordService } from '@app/core/work-record';

@Component({
  selector: 'rolx-month-page',
  templateUrl: './month-page.component.html',
  styleUrls: ['./month-page.component.scss']
})
export class MonthPageComponent implements OnInit {

  month$ = new BehaviorSubject<Dayjs>(dayjs());
  records$ = this.month$.pipe(
    switchMap(m => this.workRecordService.getMonth(m))
  );

  constructor(private workRecordService: WorkRecordService) {}

  ngOnInit() {
  }

  previous() {
    this.month$.next(this.month$.value.subtract(1, 'month'));
  }

  next() {
    this.month$.next(this.month$.value.add(1, 'month'));
  }

}
