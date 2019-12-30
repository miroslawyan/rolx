import { FormControl, FormGroup } from '@angular/forms';
import { Duration, DurationValidators, TimeFormControl, TimeOfDay, TimeOfDayValidators } from '@app/core/util';
import { RecordEntry } from '@app/work-record/core';
import { combineLatest, Subscription } from 'rxjs';
import { distinctUntilChanged, map, startWith } from 'rxjs/operators';

export class FormRow {

  readonly beginEndBased = new FormControl(false);
  readonly begin = TimeFormControl.createTimeOfDay();
  readonly end = TimeFormControl.createTimeOfDay();
  readonly pause = TimeFormControl.createDuration(null, DurationValidators.min(Duration.Zero));
  readonly duration = TimeFormControl.createDuration(null, DurationValidators.min(Duration.Zero));
  readonly comment = new FormControl('');

  readonly group = new FormGroup({
    beginEndBased: this.beginEndBased,
    begin: this.begin,
    end: this.end,
    pause: this.pause,
    duration: this.duration,
    comment: this.comment,
  });

  private subscription: Subscription;

  constructor(entry?: RecordEntry | null) {
    this.beginEndBased.valueChanges
      .subscribe(() => this.updateMode());

    if (entry) {
      this.beginEndBased.setValue(entry.isBeginEndBased);

      this.duration.setValue(entry.duration);
      this.begin.setValue(entry.begin);
      this.end.setValue(entry.end);
      this.pause.setValue(entry.pause);
      this.comment.setValue(entry.comment);
    } else {
      this.updateMode();
    }
  }

  get hasDuration(): boolean {
    const duration = this.duration.value;
    return duration && !duration.isZero;
  }

  private get isBeginEndBased() { return !!this.beginEndBased.value; }
  private get commentValue() { return this.comment.value.trim(); }

  private static toDuration(begin: TimeOfDay, end: TimeOfDay, pause: Duration): Duration {
    return begin && end
      ? end.sub(begin).sub(pause ? pause : Duration.Zero)
      : Duration.Zero;
  }

  toEntry(): RecordEntry {
    const entry = new RecordEntry();

    entry.duration = this.duration.value;
    entry.begin = this.begin.value;
    entry.pause = this.pause.value;
    entry.comment = this.commentValue;

    return entry;
  }

  resetComment() {
    this.comment.setValue(this.commentValue);
  }

  private updateMode() {
    if (this.isBeginEndBased) {
      this.enterBeginEndBaseMode();
    } else {
      this.enterDurationBasedMode();
    }
  }

  private enterBeginEndBaseMode() {
    this.unsubscribe();

    this.begin.enable();
    this.end.enable();
    this.pause.enable();
    this.duration.disable();

    const begin$ = this.begin.typedValue$.pipe(
      startWith(null),
      distinctUntilChanged(),
    );
    const end$ = this.end.typedValue$.pipe(
      startWith(null),
      distinctUntilChanged(),
    );
    const pause$ = this.pause.typedValue$.pipe(
      startWith(null),
      distinctUntilChanged(),
    );

    this.subscription = combineLatest([begin$, end$, pause$]).pipe(
      map(([b, e, p]) => FormRow.toDuration(b, e, p)),
      )
      .subscribe(d => this.duration.setValue(d));

    this.subscription.add(combineLatest([begin$, pause$])
      .subscribe(([b, p]) => this.setEndMin(b, p)));

    this.subscription.add(combineLatest([begin$, end$])
      .subscribe(([b, e]) => this.setPauseMax(b, e)));
  }

  private setEndMin(begin: TimeOfDay, pause: Duration) {
    if (begin) {
      const endMin = pause ? begin.add(pause) : begin;
      this.end.setValidators(TimeOfDayValidators.min(endMin));
    } else {
      this.end.clearValidators();
    }

    this.end.updateValueAndValidity();
  }

  private setPauseMax(begin: TimeOfDay, end: TimeOfDay) {
    if (begin && end && end.seconds >= begin.seconds) {
      this.pause.setValidators(DurationValidators.max(end.sub(begin)));
    } else {
      this.pause.clearValidators();
    }

    this.pause.updateValueAndValidity();
  }

  private enterDurationBasedMode() {
    this.unsubscribe();

    this.begin.setValue(null);
    this.begin.disable();

    this.end.setValue(null);
    this.end.disable();

    this.pause.setValue(null);
    this.pause.disable();

    this.duration.enable();
  }

  private unsubscribe() {
    if (this.subscription) {
      this.subscription.unsubscribe();
      this.subscription = null;
    }
  }

}
