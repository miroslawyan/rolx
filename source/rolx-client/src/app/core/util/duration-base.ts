export class DurationBase<T extends DurationBase<T>> {
  protected static readonly SecondsPerHour = 3600;
  protected static readonly SecondsPerMinute = 60;
  protected static readonly SecondsPerDay = 24 * DurationBase.SecondsPerHour;

  constructor(public readonly seconds: number = 0) {
  }

  get hours(): number {
    return this.seconds / DurationBase.SecondsPerHour;
  }

  toString(): string {
    if (!this.isValid) {
      return '-';
    }

    const hours = this.hours;
    const wholeHours = Math.abs(Math.trunc(hours));
    const wholeMinutes = Math.abs(Math.round((hours % 1) * 60));
    const hasSign = this.seconds < 0 && (wholeHours !== 0 || wholeMinutes !== 0);

    return `${hasSign ? '-' : ''}${wholeHours}:${wholeMinutes.toString(10).padStart(2, '0')}`;
  }

  get isValid(): boolean {
    return !Number.isNaN(this.seconds);
  }

  isSame(other: T): boolean {
    return this.seconds === other.seconds;
  }
}
