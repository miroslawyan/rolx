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

  phases: Phase[];
  rows: Phase[];

  private recordsShadow: Record[];

  constructor() { }

  get records(): Record[] {
    return this.recordsShadow;
  }

  @Input()
  set records(value: Record[]) {
    this.recordsShadow = value;

    const allPhases = this.records.flatMap(r => r.entries.map(e => e.phase));
    this.phases = [...new Map(allPhases.map(ph => [ph.id, ph])).values()]
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
    this.phases.push(phase);
    this.resetRows();
  }

  private resetRows() {
    this.rows = [...this.phases];
  }

}
