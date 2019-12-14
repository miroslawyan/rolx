import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material';
import { Phase } from '@app/account/core';
import { Duration, DurationValidators } from '@app/core/util';
import { Record, RecordEntry, WorkRecordService } from '@app/work-record/core';

@Component({
  selector: 'rolx-entries-edit',
  templateUrl: './entries-edit.component.html',
  styleUrls: ['./entries-edit.component.scss'],
})
export class EntriesEditComponent implements OnInit {

  @Input()
  record: Record;

  @Input()
  phase: Phase;

  entries: RecordEntry[] = [];
  errorStateMatcher = new ShowOnDirtyErrorStateMatcher();

  form = this.fb.group({
    duration: ['', [
      DurationValidators.isValid,
      DurationValidators.min(Duration.Zero),
    ]],
  });

  constructor(private fb: FormBuilder, private workRecordService: WorkRecordService) { }

  get durationControl() {
    return this.form.controls.duration;
  }

  ngOnInit() {
    this.entries = this.record.entriesOf(this.phase);
    if (!this.entries.length) {
      const entry = new RecordEntry();
      entry.phase = this.phase;

      this.entries.push(entry);
      this.record.entries.push(entry);
    }

    this.cancel();
  }

  get totalDuration(): Duration {
    return new Duration(
      this.entries.reduce(
        (sum, e) => sum + e.duration.hours, 0));
  }

  checkIfLeavingIsAllowed() {
    return this.durationControl.valid;
  }

  commit() {
    if (this.entries.length !== 1 || !this.durationControl.dirty) {
      return;
    }

    if (this.form.invalid) {
      this.cancel();
      return;
    }

    const formValue = this.durationControl.value;
    const duration = Duration.parse(formValue, true);
    if (!duration.isValid) {
      console.warn('invalid duration', formValue);
      this.cancel();
      return;
    }

    const targetEntry = this.entries[0];
    if (duration.isSame(targetEntry.duration)) {
      this.cancel();
      return;
    }

    targetEntry.duration = duration;
    this.workRecordService.update(this.record)
      .subscribe(() => this.cancel());
  }

  cancel() {
    if (this.entries.length !== 1) {
      return;
    }

    const duration = this.entries[0].duration;
    this.durationControl.reset(!duration.isZero ? duration : '');
  }

}
