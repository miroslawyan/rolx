import { Component, Input } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Holiday } from '@app/records/core/holiday';

@Component({
  selector: 'rolx-holiday-table',
  templateUrl: './holiday-table.component.html',
  styleUrls: ['./holiday-table.component.scss'],
})
export class HolidayTableComponent {
  readonly dataSource = new MatTableDataSource<Holiday>();
  readonly displayedColumns = ['name', 'date', 'weekday'];

  @Input()
  set holidays(value: Holiday[]) {
    this.dataSource.data = value;
  }
}
