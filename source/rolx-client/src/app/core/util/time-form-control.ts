import { FormControl, ValidatorFn } from '@angular/forms';
import { Duration } from './duration';
import { DurationValidators } from './duration-validators';
import { StrongTypedFormControl } from './strong-typed-form-control';
import { TimeOfDay } from './time-of-day';
import { TimeOfDayValidators } from './time-of-day-validators';

export class TimeFormControl  extends FormControl {

  static createDuration(value?: Duration | null, validator?: ValidatorFn | ValidatorFn[] | null): StrongTypedFormControl<Duration> {
    const control = new StrongTypedFormControl(
      v => Duration.parse(v),
      [
        DurationValidators.isValid,
      ],
      value);

    control.setValidators(validator);
    return control;
  }

  static createTimeOfDay(value?: TimeOfDay | null, validator?: ValidatorFn | ValidatorFn[] | null): StrongTypedFormControl<TimeOfDay> {
    const control = new StrongTypedFormControl(
      v => TimeOfDay.parse(v),
      [
        TimeOfDayValidators.isValid,
      ],
      value);

    control.setValidators(validator);
    return control;
  }

}
