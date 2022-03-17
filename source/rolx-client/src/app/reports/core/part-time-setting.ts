import { TransformAsIsoDate } from '@app/core/util/iso-date';
import { assertDefined } from '@app/core/util/utils';
import * as moment from 'moment';

export class PartTimeSetting {
  @TransformAsIsoDate()
  startDate!: moment.Moment;

  factor!: number;

  validateModel(): void {
    assertDefined(this, 'startDate');
    assertDefined(this, 'factor');
  }
}
