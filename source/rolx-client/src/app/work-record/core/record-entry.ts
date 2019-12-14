import { Duration, TransformAsDuration } from '@app/core/util';

export class RecordEntry {

  id: number;

  phaseId: number;

  @TransformAsDuration()
  duration = Duration.Zero;

}
