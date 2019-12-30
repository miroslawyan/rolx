import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material';
import { Duration, DurationValidators } from '@app/core/util';
import { RecordEntry } from '@app/work-record/core';

@Component({
  selector: 'rolx-entries-edit',
  templateUrl: './entries-edit.component.html',
  styleUrls: ['./entries-edit.component.scss'],
})
export class EntriesEditComponent implements OnInit {

  private entryShadow: RecordEntry | null = null;

  @ViewChild('input', {static: false})
  private inputElement: ElementRef;

  @Input()
  get entry() {
    return this.entryShadow;
  }
  set entry(value: RecordEntry | null) {
    this.entryShadow = value;
    this.cancel();
  }

  @Output()
  more = new EventEmitter();

  @Output()
  changed = new EventEmitter<RecordEntry>();

  errorStateMatcher = new ShowOnDirtyErrorStateMatcher();

  form = this.fb.group({
    duration: ['', [
      DurationValidators.isValid,
      DurationValidators.min(Duration.Zero),
    ]],
  });

  constructor(private fb: FormBuilder) { }

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

    const editedEntry = new RecordEntry();
    editedEntry.duration = duration;
    this.changed.emit(editedEntry);
  }

  cancel() {
    const duration = this.entry ? this.entry.duration : Duration.Zero;
    this.durationControl.reset(!duration.isZero ? duration : '');
  }

}
