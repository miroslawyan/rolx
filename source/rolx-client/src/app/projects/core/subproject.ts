import { assertDefined } from '@app/core/util/utils';
import { SubprojectShallow } from '@app/projects/core/subproject-shallow';
import { Type } from 'class-transformer';
import * as moment from 'moment';

import { Activity } from './activity';

export class Subproject extends SubprojectShallow {
  fullName!: string;
  projectNumber!: number;
  number!: number;
  managerId?: string;

  @Type(() => Activity)
  activities: Activity[] = [];

  override validateModel(): void {
    super.validateModel();

    assertDefined(this, 'fullName');
    assertDefined(this, 'projectNumber');
    assertDefined(this, 'number');
    assertDefined(this, 'activities');

    this.activities.forEach((a) => a.validateModel());
  }

  get nextNumber() {
    return Math.max(...this.activities.map((ph) => ph.number), 0) + 1;
  }

  get startDate(): moment.Moment | undefined {
    if (this.activities.length === 0) {
      return undefined;
    }

    return moment.min(this.activities.map((activity) => activity.startDate));
  }

  addActivity(): Activity {
    const activity = new Activity();
    activity.startDate = moment();
    activity.number = this.nextNumber;
    activity.fullName = '';
    activity.fullNumber = '';
    activity.billabilityName = '';
    this.activities.push(activity);
    return activity;
  }
}
