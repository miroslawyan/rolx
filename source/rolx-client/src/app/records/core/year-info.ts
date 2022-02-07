import { assertDefined } from '@app/core/util/utils';
import { Type } from 'class-transformer';

import { Holiday } from './holiday';
import { MonthlyWorkTime } from './monthly-work-time';

export class YearInfo {
  @Type(() => Holiday)
  holidays: Holiday[] = [];

  @Type(() => MonthlyWorkTime)
  monthlyWorkTimes: MonthlyWorkTime[] = [];

  validateModel(): void {
    assertDefined(this, 'holidays');
    assertDefined(this, 'monthlyWorkTimes');

    this.holidays.forEach((h) => h.validateModel());
    this.monthlyWorkTimes.forEach((w) => w.validateModel());
  }
}
