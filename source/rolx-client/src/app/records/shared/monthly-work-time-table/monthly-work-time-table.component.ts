import { Component, Input } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Duration } from '@app/core/util/duration';
import { MonthlyWorkTime } from '@app/records/core/monthly-work-time';

@Component({
  selector: 'rolx-monthly-work-time-table',
  templateUrl: './monthly-work-time-table.component.html',
  styleUrls: ['./monthly-work-time-table.component.scss'],
})
export class MonthlyWorkTimeTableComponent {
  readonly dataSource = new MatTableDataSource<MonthlyWorkTime>();
  displayedColumns: string[] = ['month', 'days', 'hours'];

  @Input()
  set monthlyWorkTimes(value: MonthlyWorkTime[]) {
    this.dataSource.data = value;
  }

  getTotalWorkDays(): number {
    return this.dataSource.data.reduce(
      (accumulatedDays, currentDay) => accumulatedDays + currentDay.days,
      0,
    );
  }

  getTotalWorkHours(): Duration {
    let totalWorkingHours = new Duration();
    for (const data of this.dataSource.data) {
      totalWorkingHours = totalWorkingHours.add(data.hours);
    }
    return totalWorkingHours;
  }
}
