import { Component, Input, OnInit } from '@angular/core';

import { DayType, Record } from '@app/core/work-record';

@Component({
  selector: 'rolx-month-table',
  templateUrl: './month-table.component.html',
  styleUrls: ['./month-table.component.scss']
})
export class MonthTableComponent implements OnInit {

  readonly DayType = DayType;

  @Input()
  records: Record[];

  displayedColumns: string[] = ['date', 'name'];

  constructor() { }

  ngOnInit() {
  }

}
