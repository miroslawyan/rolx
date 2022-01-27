import { Duration, TransformAsDuration } from '@app/core/util/duration';
import { TimeOfDay, TransformAsTimeOfDay } from '@app/core/util/time-of-day';

export class RecordEntry {

  phaseId: number;

  @TransformAsDuration()
  duration = Duration.Zero;

  @TransformAsTimeOfDay()
  begin: TimeOfDay | null;

  @TransformAsDuration()
  pause: Duration | null;

  comment: string | null;

  get end(): TimeOfDay | null {
    if (!this.begin) {
      return null;
    }

    const grossDuration = this.pause ? this.duration.add(this.pause) : this.duration;
    return this.begin.add(grossDuration);
  }

  get isDurationOnly(): boolean {
    return !this.begin && !this.pause && !this.hasComment;
  }

  get isBeginEndBased(): boolean {
    return !!this.begin || !!this.pause;
  }

  get hasComment(): boolean {
    return this.comment != null && this.comment !== '';
  }

}
