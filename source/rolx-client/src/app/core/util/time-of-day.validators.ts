import { AbstractControl } from '@angular/forms';

import { TimeOfDay } from './time-of-day';

export class TimeOfDayValidators {
  static isValid(control: AbstractControl) {
    const candidate = TimeOfDay.parse(control.value, true);
    if (!candidate.isValid) {
      return { valid: true };
    }

    return null;
  }

  static min(value: TimeOfDay) {
    return (control: AbstractControl) => {
      const candidate = TimeOfDay.parse(control.value);
      if (candidate.isValid && candidate.seconds < value.seconds) {
        return { min: true };
      }

      return null;
    };
  }

  static max(value: TimeOfDay) {
    return (control: AbstractControl) => {
      const candidate = TimeOfDay.parse(control.value);
      if (candidate.isValid && candidate.seconds > value.seconds) {
        return { max: true };
      }

      return null;
    };
  }
}
