import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { Duration, DurationValidators, TimeFormControl } from '@app/core/util';

@Component({
  selector: 'rolx-duration-edit',
  templateUrl: './duration-edit.component.html',
  styleUrls: ['./duration-edit.component.scss'],
})
export class DurationEditComponent implements OnInit {

  private valueShadow = Duration.Zero;

  @ViewChild('input')
  private inputElement: ElementRef;

  @Input()
  get value() {
    return this.valueShadow;
  }
  set value(value: Duration) {
    this.valueShadow = value ? value : Duration.Zero;
    this.cancel();
  }

  @Output()
  more = new EventEmitter();

  @Output()
  changed = new EventEmitter<Duration>();

  readonly errorStateMatcher = new ShowOnDirtyErrorStateMatcher();

  readonly control = TimeFormControl.createDuration(null, DurationValidators.min(Duration.Zero));
  readonly form = new FormGroup({
    duration: this.control,
  });

  ngOnInit() {
    this.cancel();
  }

  enter() {
    this.inputElement.nativeElement.focus();
  }

  checkIfLeavingIsAllowed() {
    return this.control.valid;
  }

  commit() {
    if (!this.control.dirty) {
      return;
    }

    if (this.form.invalid) {
      this.cancel();
      return;
    }

    const editedValue = this.control.value ? this.control.value : Duration.Zero;
    if (this.value.isSame(editedValue)) {
      this.cancel();
      return;
    }

    this.changed.emit(editedValue);
  }

  cancel() {
    this.control.reset(!this.value.isZero ? this.value : '');
  }

}
