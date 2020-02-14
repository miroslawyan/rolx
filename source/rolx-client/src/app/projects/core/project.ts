import { Type } from 'class-transformer';
import moment from 'moment';
import { Phase } from './phase';

export class Project {

  id: number;
  number: string;
  name: string;

  @Type(() => Phase)
  phases: Phase[] = [];

  get nextNumber() {
    return Math.max(...this.phases.map(ph => ph.number), 0) + 1;
  }

  addPhase(): Phase {
    const phase = new Phase();
    phase.startDate = moment();
    phase.number = this.nextNumber;
    this.phases.push(phase);
    return phase;
  }
}
