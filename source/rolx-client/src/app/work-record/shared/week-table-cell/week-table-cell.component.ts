import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Phase } from '@app/account/core';
import { GridCoordinates, GridNavigationService } from '@app/core/grid-navigation';
import { Duration } from '@app/core/util';
import { Record, RecordEntry } from '@app/work-record/core';
import { EntriesEditComponent } from '@app/work-record/shared';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'rolx-week-table-cell',
  templateUrl: './week-table-cell.component.html',
  styleUrls: ['./week-table-cell.component.scss'],
})
export class WeekTableCellComponent implements OnInit, OnDestroy {

  private readonly subscription = new Subscription();
  private coordinates = new GridCoordinates(undefined, undefined);

  @ViewChild(EntriesEditComponent, {static: false})
  private entriesEdit: EntriesEditComponent;

  @Input()
  record: Record;

  @Input()
  phase: Phase;

  @Input()
  get row(): number {
    return this.coordinates.row;
  }
  set row(value: number) {
    this.coordinates = new GridCoordinates(this.coordinates.column, value);
  }

  @Input()
  get column(): number {
    return this.coordinates.column;
  }
  set column(value: number) {
    this.coordinates = new GridCoordinates(value, this.coordinates.row);
  }

  entries: RecordEntry[] = [];

  constructor(private gridNavigationService: GridNavigationService) { }

  get isEditable(): boolean {
    return this.entries.length === 1 && this.isPhaseOpen;
  }

  get isPhaseOpen(): boolean {
    return this.phase.isOpenAt(this.record.date);
  }

  get totalDuration(): Duration {
    return new Duration(
      this.entries.reduce(
        (sum, e) => sum + e.duration.hours, 0));
  }

  ngOnInit() {
    this.subscription.add(
      this.gridNavigationService.coordinates$
        .pipe(
          filter(c => this.coordinates.isSame(c)),
        )
        .subscribe(c => this.entriesEdit ? this.entriesEdit.enter() : this.gridNavigationService.navigateTo(c.down())));

    this.entries = this.record.entriesOf(this.phase);
    if (!this.entries.length) {
      const entry = new RecordEntry();
      entry.phaseId = this.phase.id;

      this.entries.push(entry);
      this.record.entries.push(entry);
    }
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  up() {
    this.gridNavigationService.navigateTo(this.coordinates.up());
  }

  down() {
    this.gridNavigationService.navigateTo(this.coordinates.down());
  }

}
