import { HttpErrorResponse } from '@angular/common/http';
import { FormGroup, ValidationErrors } from '@angular/forms';

import { PropertyErrors } from './property-errors';

export class ErrorResponse {
  constructor(private httpErrorResponse: HttpErrorResponse) {}

  tryToHandleWith(formGroup: FormGroup): boolean {
    if (this.httpErrorResponse.status !== 400) {
      console.warn('unable to handle response', this.httpErrorResponse);
      return false;
    }

    try {
      const propertyErrors = ErrorResponse.camelize(this.httpErrorResponse.error.errors);
      const formControlNames = Object.keys(formGroup.controls);
      const propertyErrorNames = Object.keys(propertyErrors);

      const surplusErrors = propertyErrorNames.filter((n) => formControlNames.indexOf(n) === -1);
      if (surplusErrors.length > 0) {
        console.warn(
          'validation has surplus error properties',
          surplusErrors,
          this.httpErrorResponse,
        );
        return false;
      }

      formControlNames.forEach((n) =>
        formGroup.controls[n].setErrors(ErrorResponse.validationErrorsFrom(propertyErrors[n])),
      );
      return true;
    } catch (e) {
      console.warn('unable to process errors', e);
      return false;
    }
  }

  private static camelize(source: PropertyErrors): PropertyErrors {
    const result: PropertyErrors = {};
    Object.keys(source).forEach((p) => {
      result[p.substring(0, 1).toLowerCase() + p.substring(1)] = source[p];
    });

    return result;
  }

  private static validationErrorsFrom(errors: string[]): ValidationErrors | null {
    if (errors == null) {
      return null;
    }

    return errors.reduce((obj: { [name: string]: boolean }, e) => {
      obj[e] = true;
      return obj;
    }, {});
  }
}
