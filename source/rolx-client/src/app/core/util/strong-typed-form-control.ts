import { FormControl, ValidatorFn } from '@angular/forms';
import { Observable } from 'rxjs';
import { filter, map, share } from 'rxjs/operators';

export class StrongTypedFormControl<T> extends FormControl {
  readonly typedValue$: Observable<T | null> = this.valueChanges.pipe(
    filter(() => this.valid),
    map((v) => (!this.isEmpty ? this.parserFn(v) : null)),
    share(),
  );

  constructor(
    private parserFn: (value: any) => T,
    private baseValidators: ValidatorFn[],
    value?: T | null,
  ) {
    super(value, null);
    this.clearValidators();

    this.typedValue$.subscribe((s) =>
      this.setValue(s, {
        emitEvent: false,
        emitModelToViewChange: false,
      }),
    );
  }

  private get isEmpty() {
    return this.value == null || this.value === '';
  }

  resetValueIfValid() {
    if (this.valid) {
      this.reset(this.value, {
        emitEvent: false,
      });
    }
  }

  override setValidators(newValidator: ValidatorFn | ValidatorFn[] | null) {
    if (!newValidator) {
      this.clearValidators();
      return;
    }

    const newValidators = Array.isArray(newValidator) ? newValidator : [newValidator];

    super.setValidators([...this.baseValidators, ...newValidators]);
  }

  override clearValidators() {
    super.setValidators(this.baseValidators);
  }
}
