import { assertDefined } from '@app/core/util/utils';
import { Type } from 'class-transformer';
import * as moment from 'moment';

import { Phase } from './phase';

export class Project {
  id!: number;
  number!: string;
  name!: string;

  @Type(() => Phase)
  phases: Phase[] = [];

  validateModel(): void {
    assertDefined(this, 'id');
    assertDefined(this, 'number');
    assertDefined(this, 'name');
    assertDefined(this, 'phases');

    this.phases.forEach((ph) => ph.validateModel());
  }

  get nextNumber() {
    return Math.max(...this.phases.map((ph) => ph.number), 0) + 1;
  }

  addPhase(): Phase {
    const phase = new Phase();
    phase.startDate = moment();
    phase.number = this.nextNumber;
    this.phases.push(phase);
    return phase;
  }
}
