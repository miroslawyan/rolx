import { TransformAsIsoDate } from '@app/core/util/iso-date';
import { assertDefined } from '@app/core/util/utils';
import * as moment from 'moment';

export class Holiday {
  @TransformAsIsoDate()
  date!: moment.Moment;
  name!: string;

  validateModel(): void {
    assertDefined(this, 'date');
    assertDefined(this, 'name');
  }
}
