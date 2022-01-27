import { Type } from 'class-transformer';
import { Holiday } from './holiday';
import { MonthlyWorkTime } from './monthly-work-time';

export class YearInfo {
  @Type(() => Holiday)
  holidays: Holiday[] = [];

  @Type(() => MonthlyWorkTime)
  monthlyWorkTimes: MonthlyWorkTime[] = [];
}
