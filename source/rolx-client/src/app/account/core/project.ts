import moment from 'moment';
import { Phase } from './phase';

export class Project {

  id: number;
  number: string;
  name: string;
  phases: Phase[] = [];

  static fromJson(data: any): Project {
    const self = new Project();

    Object.assign(self, data);

    self.phases = data.phases.map(p => Phase.fromJson(p));

    return self;
  }

  get nextNumber() {
    return Math.max(...this.phases.map(ph => ph.number), 0) + 1;
  }

  toJson(): any {
    const data = {
      phases: undefined,
    };

    Object.assign(data, this);

    data.phases = this.phases.map(p => p.toJson());

    return data;
  }

  addPhase(): Phase {
    const phase = new Phase();
    phase.startDate = moment();
    phase.number = this.nextNumber;
    this.phases.push(phase);
    return phase;
  }
}
