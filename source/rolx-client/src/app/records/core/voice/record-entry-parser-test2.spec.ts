import { Duration } from '@app/core/util/duration';
import { TimeOfDay } from '@app/core/util/time-of-day';
import { RecordEntry } from '@app/records/core/record-entry';

import { ParserResult } from './parser-result';
import { RecordEntryParser } from './record-entry-parser';

const TestCase = (
  text: string,
  duration: string,
  begin?: string,
  comment?: string,
): [string, RecordEntry] => {
  const entry = new RecordEntry();
  entry.duration = Duration.parse(duration);
  entry.begin = begin != null ? TimeOfDay.parse(begin) : undefined;
  entry.comment = comment ?? '';

  return [text, entry];
};

const testCases = [
  TestCase('von 8:15 Uhr bis 9:15 Uhr Kommentar das ist toll', '01:00', '08:15', 'das ist toll'),
  TestCase('2 stunden', '2:00', undefined, ''),
  TestCase('2 stunden 15 minuten', '2:15', undefined, ''),
  TestCase('2 stunden kommentar hallo welt', '2:00', undefined, 'hallo welt'),
  TestCase('2 stunden 15 minuten kommentar zu lange', '2:15', undefined, 'zu lange'),
  TestCase('von 8:30 uhr bis 9:45 uhr kommentar bis pause', '1:15', '8:30', 'bis pause'),
  TestCase('zwei stunden dreissig minuten', '2:30', undefined, ''),
  TestCase('zwei stunden dreissig', '2:30', undefined, ''),
  TestCase('16 Uhr bis 17 Uhr', '01:00', '16:00'),
  TestCase('10 15 bis 11.30', '01:15', '10:15'),
  TestCase('eine Stunde 20 Minuten', '01:20'),
];

const testEntry = (text: string, expected: RecordEntry, parser: (text: string) => ParserResult) => {
  it(`parses "${text}" to non-null entry`, () => {
    expect(parser(text).entry).toBeDefined();
  });
};

const testDuration = (
  text: string,
  expected: RecordEntry,
  parser: (text: string) => ParserResult,
) => {
  it(`parses "${text}" duration`, () => {
    expect(parser(text).entry?.duration?.isSame(expected.duration)).toBeTrue();
  });
};

const testBegin = (text: string, expected: RecordEntry, parser: (text: string) => ParserResult) => {
  it(`parses "${text}" begin`, () => {
    if (expected.begin != null) {
      expect(parser(text).entry?.begin?.isSame(expected.begin)).toBeTrue();
    }
  });
};

const testComment = (
  text: string,
  expected: RecordEntry,
  parser: (text: string) => ParserResult,
) => {
  it(`parses "${text}" comment`, () => {
    if (expected.comment != null) {
      expect(parser(text).entry?.comment).toBe(expected.comment);
    }
  });
};

const testAll = (text: string, expected: RecordEntry, parser: (text: string) => ParserResult) => {
  testEntry(text, expected, parser);
  testDuration(text, expected, parser);
  testBegin(text, expected, parser);
  testComment(text, expected, parser);
};

describe('Parser Test', () => {
  testCases.forEach(([text, expected]) =>
    testAll(text, expected, (t) => RecordEntryParser.parse(t)),
  );
});
