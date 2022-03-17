import { Duration, TransformAsDuration } from '@app/core/util/duration';
import { TransformAsIsoDate } from '@app/core/util/iso-date';
import { assertDefined } from '@app/core/util/utils';
import * as moment from 'moment';

export class BalanceCorrection {
  @TransformAsIsoDate()
  date!: moment.Moment;

  @TransformAsDuration()
  overtime!: Duration;

  @TransformAsDuration()
  vacation!: Duration;

  validateModel(): void {
    assertDefined(this, 'date');
    assertDefined(this, 'overtime');
    assertDefined(this, 'vacation');
  }
}
