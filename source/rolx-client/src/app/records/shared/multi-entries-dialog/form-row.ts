import { FormControl, FormGroup } from '@angular/forms';
import { Duration } from '@app/core/util/duration';
import { DurationValidators } from '@app/core/util/duration.validators';
import { TimeFormControl } from '@app/core/util/time-form-control';
import { TimeOfDay } from '@app/core/util/time-of-day';
import { TimeOfDayValidators } from '@app/core/util/time-of-day.validators';
import { RecordEntry } from '@app/records/core/record-entry';
import { combineLatest, Subscription } from 'rxjs';
import { distinctUntilChanged, map, startWith } from 'rxjs/operators';

export class FormRow {
  private subscription?: Subscription;

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

  constructor(entryOrIsBeginEndBased: RecordEntry | boolean) {
    this.beginEndBased.valueChanges.subscribe(() => this.updateMode());

    if (entryOrIsBeginEndBased instanceof RecordEntry) {
      const entry = entryOrIsBeginEndBased;
      this.beginEndBased.setValue(entry.isBeginEndBased);

      this.duration.setValue(entry.duration);
      this.begin.setValue(entry.begin);
      this.end.setValue(entry.end);
      this.pause.setValue(entry.pause);
      this.comment.setValue(entry.comment);
    } else {
      this.beginEndBased.setValue(entryOrIsBeginEndBased);
      this.updateMode();
    }
  }

  get hasDuration(): boolean {
    const duration = this.duration.value;
    return duration && !duration.isZero;
  }

  get isBeginEndBased() {
    return !!this.beginEndBased.value;
  }

  private get commentValue() {
    return this.comment.value?.trim();
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

  private static toDuration(
    begin: TimeOfDay | null,
    end: TimeOfDay | null,
    pause: Duration | null,
  ): Duration {
    return begin && end ? end.sub(begin).sub(pause ?? Duration.Zero) : Duration.Zero;
  }

  private updateMode() {
    if (this.isBeginEndBased) {
      this.enterBeginEndBasedMode();
    } else {
      this.enterDurationBasedMode();
    }
  }

  private enterBeginEndBasedMode() {
    this.unsubscribe();

    this.begin.enable();
    this.end.enable();
    this.pause.enable();
    this.duration.disable();

    const begin$ = this.begin.typedValue$.pipe(startWith(null), distinctUntilChanged());
    const end$ = this.end.typedValue$.pipe(startWith(null), distinctUntilChanged());
    const pause$ = this.pause.typedValue$.pipe(startWith(null), distinctUntilChanged());

    this.subscription = combineLatest([begin$, end$, pause$])
      .pipe(map(([b, e, p]) => FormRow.toDuration(b, e, p)))
      .subscribe((d) => this.duration.setValue(d));

    this.subscription.add(
      combineLatest([begin$, pause$]).subscribe(([b, p]) => this.setEndMin(b, p)),
    );

    this.subscription.add(
      combineLatest([pause$, begin$, end$]).subscribe(([p]) => this.setPauseMin(p)),
    );

    this.subscription.add(
      combineLatest([begin$, end$]).subscribe(([b, e]) => this.setPauseMax(b, e)),
    );
  }

  private setEndMin(begin: TimeOfDay | null, pause: Duration | null) {
    if (begin) {
      const endMin = pause ? begin.add(pause ?? Duration.Zero) : begin;
      this.end.setValidators(TimeOfDayValidators.min(endMin));
    } else {
      this.end.clearValidators();
    }

    this.end.updateValueAndValidity();
  }

  private setPauseMin(pause: Duration | null) {
    if (pause) {
      this.pause.setValidators(DurationValidators.min(Duration.Zero));
    } else {
      this.pause.clearValidators();
    }

    this.pause.updateValueAndValidity();
  }

  private setPauseMax(begin: TimeOfDay | null, end: TimeOfDay | null) {
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
    this.subscription?.unsubscribe();
    this.subscription = undefined;
  }
}
