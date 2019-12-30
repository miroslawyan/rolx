import { Transform } from 'class-transformer';
import { DurationBase } from './duration-base';
import { allowNull } from './utils';

export class Duration extends DurationBase<Duration> {
  static readonly Zero = new Duration();
  static readonly Pattern = /^(-?)(?:(\d+):([0-5]?\d)|(\d*\.?\d*))$/;
  static readonly PatternGroups = {
    Sign: 1,
    Hours: 2,
    Minutes: 3,
    DecimalHours: 4,
  };

  static fromHours(hours: number) {
    return new Duration(Math.round(hours * DurationBase.SecondsPerHour));
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

    if (match[Duration.PatternGroups.DecimalHours]) {
      return Duration.fromHours(Number.parseFloat(time));
    }

    const hours = Number.parseInt(match[Duration.PatternGroups.Hours], 10);
    const minutes = Number.parseInt(match[Duration.PatternGroups.Minutes], 10);

    const seconds = hours * DurationBase.SecondsPerHour + minutes * DurationBase.SecondsPerMinute;
    return new Duration(match[Duration.PatternGroups.Sign] ? -seconds : seconds);
  }

  get isZero(): boolean {
    return this.isSame(Duration.Zero);
  }

  add(other: Duration): Duration {
    return new Duration(this.seconds + other.seconds);
  }

  sub(other: Duration): Duration {
    return new Duration(this.seconds - other.seconds);
  }
}

export function TransformAsDuration(): (target: any, key: string) => void {
  const toClass = Transform(allowNull((v: number)  => new Duration(v)), { toClassOnly: true });
  const toPlain = Transform(allowNull((v: Duration) => v.seconds), { toPlainOnly: true });

  return (target: any, key: string) => {
    toClass(target, key);
    toPlain(target, key);
  };
}
