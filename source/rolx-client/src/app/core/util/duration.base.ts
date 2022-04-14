export class DurationBase<T extends DurationBase<T>> {
  constructor(public readonly seconds: number = 0) {}

  get hours(): number {
    return this.seconds / DurationBase.SecondsPerHour;
  }

  toString(): string {
    if (!this.isValid) {
      return '-';
    }

    const minutes = Math.round(this.seconds / 60);
    const wholeHours = Math.abs(Math.trunc(minutes / 60));
    const wholeMinutes = Math.abs(minutes % 60);
    const hasSign = this.seconds < 0 && minutes !== 0;

    return `${hasSign ? '-' : ''}${wholeHours}:${wholeMinutes.toString(10).padStart(2, '0')}`;
  }

  get isValid(): boolean {
    return !Number.isNaN(this.seconds);
  }

  isSame(other: T): boolean {
    return other && this.seconds === other.seconds;
  }

  protected static readonly SecondsPerHour = 3600;
  protected static readonly SecondsPerMinute = 60;
  protected static readonly SecondsPerDay = 24 * DurationBase.SecondsPerHour;
}
