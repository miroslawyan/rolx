import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material';
import { Duration, DurationValidators } from '@app/core/util';
import { Record, RecordEntry, WorkRecordService } from '@app/work-record/core';

@Component({
  selector: 'rolx-entries-edit',
  templateUrl: './entries-edit.component.html',
  styleUrls: ['./entries-edit.component.scss'],
})
export class EntriesEditComponent implements OnInit {

  @ViewChild('input', {static: false})
  private inputElement: ElementRef;

  @Input()
  record: Record;

  @Input()
  entry: RecordEntry;

  errorStateMatcher = new ShowOnDirtyErrorStateMatcher();

  form = this.fb.group({
    duration: ['', [
      DurationValidators.isValid,
      DurationValidators.min(Duration.Zero),
    ]],
  });

  constructor(private fb: FormBuilder, private workRecordService: WorkRecordService) { }

  ngOnInit() {
    this.cancel();
  }

  get durationControl() {
    return this.form.controls.duration;
  }

  enter() {
    this.inputElement.nativeElement.focus();
  }

  checkIfLeavingIsAllowed() {
    return this.durationControl.valid;
  }

  commit() {
    if (!this.durationControl.dirty) {
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

    if (duration.isSame(this.entry.duration)) {
      this.cancel();
      return;
    }

    this.entry.duration = duration;
    this.workRecordService.update(this.record)
      .subscribe(() => this.cancel());
  }

  cancel() {
    const duration = this.entry.duration;
    this.durationControl.reset(!duration.isZero ? duration : '');
  }

}
