import { Component, Input } from '@angular/core';
import { Phase } from '@app/account/core';
import { Record } from '@app/work-record/core';

@Component({
  selector: 'rolx-week-table',
  templateUrl: './week-table.component.html',
  styleUrls: ['./week-table.component.scss'],
})
export class WeekTableComponent {

  readonly weekdays = [
    'monday',
    'tuesday',
    'wednesday',
    'thursday',
    'friday',
    'saturday',
    'sunday',
  ];

  readonly displayedColumns: string[] = [
    'phase',
    ...this.weekdays,
  ];

  @Input()
  records: Record[] = [];

  rows: Phase[] = [];

  private phasesShadow: Phase[] = [];

  constructor() { }

  @Input()
  get phases(): Phase[] {
    return this.phasesShadow;
  }
  set phases(value: Phase[]) {
    this.phasesShadow = value
      .sort((a, b) => a.fullName.localeCompare(b.fullName));

    this.resetRows();
  }

  get isAddRowEnabled() {
    return this.rows.length ? this.rows[this.rows.length - 1] != null : true;
  }

  addRow() {
    this.rows.push(null);
  }

  addPhase(phase: Phase) {
    this.phasesShadow.push(phase);
    this.resetRows();
  }

  private resetRows() {
    this.rows = [...this.phasesShadow];
  }

}
