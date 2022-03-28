import { TimeOfDay } from '@app/core/util/time-of-day';

const legalTimes = [
  ['08:15', '8:15'],
  ['8:15', '8:15'],
  ['18:15', '18:15'],
  ['22:15', '22:15'],

  ['0:00', '0:00'],
  ['23:59', '23:59'],

  ['8', '8:00'],
  ['18', '18:00'],
  ['22', '22:00'],

  ['8:', '8:00'],
  ['18:', '18:00'],
  ['22:', '22:00'],

  ['8:0', '8:00'],
  ['18:0', '18:00'],
  ['22:0', '22:00'],

  ['8:3', '8:03'],
  ['18:3', '18:03'],
  ['22:3', '22:03'],

  ['8:', '8:00'],
  ['18:', '18:00'],
  ['22:', '22:00'],

  [':', '0:00'],
  [':2', '0:02'],
  [':23', '0:23'],

  ['0815', '8:15'],
  ['1815', '18:15'],
  ['2215', '22:15'],
];

const illegalTimes = [
  '24:01',
  '24:06',
  '24:11',
  '24:16',
  '24:56',
  '24:1',
  '24:6',
  '32:00',
  '24',
  '24:',
  '24:0',
  '24:00',
];

const testParseValid = (time: string, expected: string) => {
  it(`${time} is parsed to ${expected}`, () => {
    expect(TimeOfDay.parse(time).toString()).toBe(expected);
  });
};

const testParseInvalid = (time: string) => {
  it(`${time} is parsed as invalid`, () => {
    expect(TimeOfDay.parse(time).isValid).toBeFalsy();
  });
};

describe('TimeOfDay.parse()', () => {
  legalTimes.forEach((v) => testParseValid(v[0], v[1]));
  illegalTimes.forEach((v) => testParseInvalid(v));
});
