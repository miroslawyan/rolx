import { Component, Inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Duration } from '@app/core/util/duration';
import { Activity } from '@app/projects/core/activity';
import { EditLockService } from '@app/records/core/edit-lock.service';
import { Record } from '@app/records/core/record';
import { RecordEntry } from '@app/records/core/record-entry';

import { FormRow } from './form-row';

export interface MultiEntriesDialogData {
  record: Record;
  activity: Activity;
}

@Component({
  selector: 'rolx-multi-entries-dialog',
  templateUrl: './multi-entries-dialog.component.html',
  styleUrls: ['./multi-entries-dialog.component.scss'],
})
export class MultiEntriesDialogComponent implements OnInit {
  readonly isLocked = this.editLockService.isLocked(this.data.record.date);

  form = this.fb.group({
    entries: this.fb.array([]),
  });

  formRows: FormRow[] = [];
  displayedColumns: string[] = ['mode', 'begin', 'end', 'pause', 'duration', 'comment', 'tools'];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: MultiEntriesDialogData,
    private dialogRef: MatDialogRef<MultiEntriesDialogComponent>,
    private fb: FormBuilder,
    private readonly editLockService: EditLockService,
  ) {
    this.dialogRef.disableClose = true;
  }

  ngOnInit() {
    const entries = this.data.record.entriesOf(this.data.activity);

    if (entries.length) {
      entries.filter((e) => !e.duration.isZero).forEach((e) => this.addRow(e));
    } else {
      this.addRow();
    }

    if (this.isLocked) {
      this.form.disable();
    }
  }

  get totalDuration(): Duration {
    return new Duration(
      this.formRows
        .filter((r) => r.duration.value)
        .reduce((sum, r) => sum + r.duration.value.seconds, 0),
    );
  }

  submit() {
    const entries = this.formRows.filter((r) => r.hasDuration).map((r) => r.toEntry());

    const record = this.data.record.replaceEntriesOfActivity(this.data.activity, entries);
    this.dialogRef.close(record);
  }

  close() {
    this.dialogRef.close(null);
  }

  tryAddRow(index: number) {
    if (index === this.formRows.length - 1) {
      this.addRow();
    }
  }

  addRow(entry?: RecordEntry | null) {
    const row =
      entry != null
        ? new FormRow(entry)
        : new FormRow(this.formRows[this.formRows.length - 1]?.isBeginEndBased ?? true);

    (this.form.controls['entries'] as FormArray).push(row.group);
    this.formRows = [...this.formRows, row];
  }

  removeRow(row: FormRow): void {
    this.formRows = this.formRows.filter((formRow) => formRow !== row);

    const entriesControl = this.form.controls['entries'] as FormArray;
    entriesControl.removeAt(entriesControl.controls.indexOf(row.group));

    if (this.formRows.length === 0) {
      this.addRow();
    }
  }
}
