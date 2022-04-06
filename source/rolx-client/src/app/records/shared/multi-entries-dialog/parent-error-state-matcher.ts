import { AbstractControl, FormGroupDirective, NgForm } from '@angular/forms';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';

export class ParentErrorStateMatcher extends ShowOnDirtyErrorStateMatcher {
  override isErrorState(
    control: AbstractControl | null,
    form: FormGroupDirective | NgForm | null,
  ): boolean {
    if (super.isErrorState(control, form)) {
      return true;
    }

    return control?.enabled === true && control?.parent?.errors != null;
  }
}
