import { Phase } from '@app/account/core';
import { Duration, TransformAsDuration } from '@app/core/util';
import { Type } from 'class-transformer';

export class RecordEntry {

  id: number;

  @Type(() => Phase)
  phase: Phase;

  @TransformAsDuration()
  duration = Duration.Zero;

}
