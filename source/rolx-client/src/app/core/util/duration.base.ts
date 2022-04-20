export class DurationBase<T extends DurationBase<T>> {
  constructor(public readonly seconds: number = 0) {}

  get hours(): number {
    return this.seconds / DurationBase.SecondsPerHour;
  }

  toString(forcePlusSign = false): string {
    if (!this.isValid) {
      return '-';
    }

    const minutes = Math.round(this.seconds / 60);
    const wholeHours = Math.abs(Math.trunc(minutes / 60));
    const wholeMinutes = Math.abs(minutes % 60);

    let sign = '';
    if (minutes !== 0) {
      if (this.seconds > 0 && forcePlusSign) {
        sign = '+';
      } else if (this.seconds < 0) {
        sign = '-';
      }
    }

    return `${sign}${wholeHours}:${wholeMinutes.toString(10).padStart(2, '0')}`;
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
