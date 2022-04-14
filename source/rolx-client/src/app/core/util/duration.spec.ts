import { Duration } from './duration';

const secondsAndTimes: [number, string][] = [
  [0, '0:00'],
  [29, '0:00'],
  [30, '0:01'],
  [60, '0:01'],
  [89, '0:01'],
  [90, '0:02'],
  [7200, '2:00'],
  [7199, '2:00'],
  [7170, '2:00'],
  [7169, '1:59'],

  [-30, '0:00'],
  [-31, '-0:01'],
  [-60, '-0:01'],
  [-90, '-0:01'],
  [-91, '-0:02'],
];

const hoursAndTimes: [number, string][] = [
  [0, '0:00'],
  [0, '0:00'],
  [-0, '0:00'],
  [7, '7:00'],
  [4711, '4711:00'],
  [-7, '-7:00'],
  [-4711, '-4711:00'],
  [0.05, '0:03'],
  [0.95, '0:57'],
  [-0.05, '-0:03'],
  [-0.95, '-0:57'],
  [7.5, '7:30'],
  [4711.016666, '4711:01'],
  [-7.5, '-7:30'],
  [-4711.016666, '-4711:01'],
];

const testSecondsToString = (seconds: number, expected: string) => {
  it(`${seconds} s is stringified to ${expected}`, () => {
    expect(new Duration(seconds).toString()).toBe(expected);
  });
};

const testHoursToString = (hours: number, expected: string) => {
  it(`${hours} h is stringified to ${expected}`, () => {
    expect(Duration.fromHours(hours).toString()).toBe(expected);
  });
};

describe('Duration.toString()', () => {
  secondsAndTimes.forEach((v) => testSecondsToString(v[0], v[1]));
  hoursAndTimes.forEach((v) => testHoursToString(v[0], v[1]));
});

const hourStringsAndHours: [string, number][] = [
  ['0', 0],
  ['-0', 0],
  ['12.5', 12.5],
  ['-12.5', -12.5],

  ['12,5', 12.5],
  ['-12,5', -12.5],

  ['12:30', 12.5],
  ['-12:30', -12.5],

  ['12..30', 12.5],
  ['-12..30', -12.5],

  ['12,,30', 12.5],
  ['-12,,30', -12.5],
];

const testParse = (time: string, expected: number) => {
  it(`${time} is parsed as valid`, () => {
    expect(Duration.parse(time).isValid).toBeTruthy();
  });

  it(`${time} is parsed close to ${expected} h`, () => {
    expect(Duration.parse(time).hours).toBeCloseTo(expected, 5);
  });
};

describe('Duration.parse()', () => {
  hoursAndTimes.forEach((v) => testParse(v[1], v[0]));

  hourStringsAndHours.forEach((v) => testParse(v[0], v[1]));

  it('non-time string returns invalid', () => {
    expect(Duration.parse('the rain in spain').isValid).toBeFalsy();
  });
});
