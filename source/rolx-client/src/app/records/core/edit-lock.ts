import { TransformAsIsoDate } from '@app/core/util/iso-date';
import { assertDefined } from '@app/core/util/utils';
import * as moment from 'moment';

export class EditLock {
  @TransformAsIsoDate()
  date!: moment.Moment;

  validateModel(): void {
    assertDefined(this, 'date');
  }
}
