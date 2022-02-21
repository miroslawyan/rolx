import { assertDefined } from '@app/core/util/utils';
import { Type } from 'class-transformer';
import * as moment from 'moment';

import { Activity } from './activity';

export class Subproject {
  id!: number;
  number!: number;
  name!: string;
  projectNumber!: number;
  projectName!: string;
  customerName!: string;

  @Type(() => Activity)
  activities: Activity[] = [];

  fullNumber!: string;
  fullName!: string;

  validateModel(): void {
    assertDefined(this, 'id');
    assertDefined(this, 'number');
    assertDefined(this, 'name');
    assertDefined(this, 'projectNumber');
    assertDefined(this, 'projectName');
    assertDefined(this, 'customerName');
    assertDefined(this, 'activities');
    assertDefined(this, 'fullNumber');
    assertDefined(this, 'fullName');

    this.activities.forEach((a) => a.validateModel());
  }

  get nextNumber() {
    return Math.max(...this.activities.map((ph) => ph.number), 0) + 1;
  }

  addActivity(): Activity {
    const activity = new Activity();
    activity.startDate = moment();
    activity.number = this.nextNumber;
    this.activities.push(activity);
    return activity;
  }
}
