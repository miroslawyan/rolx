import { Duration, TransformAsDuration } from '@app/core/util/duration';
import { assertDefined } from '@app/core/util/utils';

export class WorkItem {
  name!: string;

  @TransformAsDuration()
  duration!: Duration;

  validateModel(): void {
    assertDefined(this, 'name');
    assertDefined(this, 'duration');
  }
}
