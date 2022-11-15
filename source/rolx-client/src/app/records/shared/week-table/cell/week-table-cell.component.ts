import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { GridCoordinates } from '@app/core/grid-navigation/grid-coordinates';
import { GridNavigationService } from '@app/core/grid-navigation/grid-navigation.service';
import { Duration } from '@app/core/util/duration';
import { assertDefined } from '@app/core/util/utils';
import { Activity } from '@app/projects/core/activity';
import { EditLockService } from '@app/records/core/edit-lock.service';
import { Record } from '@app/records/core/record';
import { RecordEntry } from '@app/records/core/record-entry';
import { VoiceToRecordEntryService } from '@app/records/core/voice/voice-to-record-entry.service';
import { DurationEditComponent } from '@app/records/shared/duration-edit/duration-edit.component';
import {
  MultiEntriesDialogComponent,
  MultiEntriesDialogData,
} from '@app/records/shared/multi-entries-dialog/multi-entries-dialog.component';
import { ParserFailedDialogComponent } from '@app/records/shared/parser-failed-dialog/parser-failed-dialog.component';
import { User } from '@app/users/core/user';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'rolx-week-table-cell',
  templateUrl: './week-table-cell.component.html',
  styleUrls: ['./week-table-cell.component.scss'],
})
export class WeekTableCellComponent implements OnInit, OnDestroy {
  private readonly subscription = new Subscription();
  private coordinates = new GridCoordinates(0, 0);

  @ViewChild(DurationEditComponent)
  private durationEdit?: DurationEditComponent;

  @ViewChild('moreButton')
  private moreButton?: MatButton;

  @Input()
  record!: Record;

  @Input()
  activity!: Activity;

  @Input()
  user!: User;

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

  @Output()
  changed = new EventEmitter<Record>();

  get entries(): RecordEntry[] {
    return this.record.entriesOf(this.activity);
  }

  get isLocked(): boolean {
    return this.editLockService.isLocked(this.record.date);
  }

  get isSimpleEditable(): boolean {
    return (
      !this.isLocked &&
      this.entries.length <= 1 &&
      this.entries.every((e) => e.isDurationOnly) &&
      this.isActivityOpen
    );
  }

  get commentsText(): string {
    return this.entries
      .map((entry) => entry.comment)
      .filter((comment) => !!comment)
      .join('\n');
  }

  get isActivityOpen(): boolean {
    return this.activity.isOpenAt(this.record.date) && this.user.isActiveAt(this.record.date);
  }

  get totalDuration(): Duration {
    return new Duration(this.entries.reduce((sum, e) => sum + e.duration.seconds, 0));
  }

  constructor(
    private readonly gridNavigationService: GridNavigationService,
    private readonly dialog: MatDialog,
    private readonly editLockService: EditLockService,
    private readonly voiceToRecordEntryService: VoiceToRecordEntryService,
  ) {}

  ngOnInit() {
    assertDefined(this, 'record');
    assertDefined(this, 'activity');
    assertDefined(this, 'user');

    this.subscription.add(
      this.gridNavigationService.coordinates$
        .pipe(filter((c) => this.coordinates.isSame(c)))
        .subscribe((c) => this.enter(c)),
    );
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

  submitSingleEntry(duration: Duration) {
    const entry = new RecordEntry();
    entry.duration = duration;

    const record = this.record.replaceEntriesOfActivity(this.activity, [entry]);
    this.changed.emit(record);
  }

  editEntries() {
    const data: MultiEntriesDialogData = {
      record: this.record,
      activity: this.activity,
    };

    this.dialog
      .open(MultiEntriesDialogComponent, {
        closeOnNavigation: true,
        data,
      })
      .afterClosed()
      .pipe(filter((r) => r != null))
      .subscribe((r) => this.changed.emit(r));
  }

  async voiceRecord(): Promise<void> {
    const result = await this.voiceToRecordEntryService.getNext();
    if (result.entry == null) {
      this.dialog.open(ParserFailedDialogComponent, {
        closeOnNavigation: true,
        data: result,
      });
      return;
    }

    result.entry.activityId = this.activity.id;
    this.record.entries.push(result.entry);
    this.changed.emit(this.record);
  }

  private enter(coordinates: GridCoordinates) {
    if (this.durationEdit) {
      this.durationEdit.enter();
    } else if (this.moreButton) {
      this.moreButton.focus();
    } else {
      this.gridNavigationService.navigateTo(coordinates.down());
    }
  }
}
