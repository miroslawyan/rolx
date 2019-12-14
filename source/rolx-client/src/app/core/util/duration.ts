import { Transform } from 'class-transformer';

export class Duration {
  private static readonly SecondsPerHour = 3600;
  private static readonly SecondsPerMinute = 60;

  static readonly Zero = new Duration();
  static readonly Pattern = /^(?<sign>-?)((?<hours>\d+):(?<minutes>[0-5]\d)|(?<decimalHours>\d*\.?\d*))$/;

  constructor(public readonly seconds: number = 0) {
  }

  static fromHours(hours: number) {
    return new Duration(Math.round(hours * Duration.SecondsPerHour));
  }

  static parse(time: string | any, zeroIfEmpty: boolean = false): Duration {

    if (time instanceof Duration) {
      return time as Duration;
    }

    if (zeroIfEmpty && (time == null || time === '')) {
      return Duration.Zero;
    }

    const match = this.Pattern.exec(time);
    if (!match) {
      return new Duration(NaN);
    }

    if (match.groups.decimalHours) {
      return Duration.fromHours(Number.parseFloat(time));
    }

    const hours = Number.parseInt(match.groups.hours, 10);
    const minutes = Number.parseInt(match.groups.minutes, 10);

    const seconds = Math.round(hours * Duration.SecondsPerHour + minutes * Duration.SecondsPerMinute);
    return new Duration(match.groups.sign ? -seconds : seconds);
  }

  get hours(): number {
    return this.seconds / Duration.SecondsPerHour;
  }

  toString(): string {
    const hours = this.hours;
    const wholeHours = Math.abs(Math.trunc(hours));
    const wholeMinutes = Math.abs(Math.round((hours % 1) * 60));
    const hasSign = this.seconds < 0 && (wholeHours !== 0 || wholeMinutes !== 0);

    return `${hasSign ? '-' : ''}${wholeHours}:${wholeMinutes.toString(10).padStart(2, '0')}`;
  }

  get isValid(): boolean {
    return !Number.isNaN(this.hours);
  }

  get isZero(): boolean {
    return this.isSame(Duration.Zero);
  }

  isSame(other: Duration): boolean {
    return this.seconds === other.seconds;
  }
}

export function TransformAsDuration(): (target: any, key: string) => void {
  const toClass = Transform(v => new Duration(v), { toClassOnly: true });
  const toPlain = Transform(v => v.seconds, { toPlainOnly: true });

  return (target: any, key: string) => {
    toClass(target, key);
    toPlain(target, key);
  };
}
