import { Duration, TimeOfDay, TransformAsDuration, TransformAsTimeOfDay } from '@app/core/util';

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
    return this.comment && this.comment !== '';
  }

}
