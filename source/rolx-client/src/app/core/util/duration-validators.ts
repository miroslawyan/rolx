import { AbstractControl } from '@angular/forms';
import { Duration } from '@app/core/util/duration';

export class DurationValidators {
  static isValid(control: AbstractControl) {
    const candidate = Duration.parse(control.value, true);
    if (!candidate.isValid) {
      return {valid: true};
    }

    return null;
  }

  static min(value: Duration) {
    return (control: AbstractControl) => {
      const candidate = Duration.parse(control.value, true);
      if (candidate.isValid && candidate.seconds < value.seconds) {
        return {min: true};
      }

      return null;
    };
  }
}
