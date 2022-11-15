import { RecordEntryParser } from './record-entry-parser';

describe('RecordEntryParser.parse()', () => {
  it('Keine Zeiten', () => expect(RecordEntryParser.parse('foo bar').success).toBeFalse());

  it('Feste Dauer in Stunden mit Kommentar - zeit', () =>
    expect(RecordEntryParser.parse('2 stunden kommentar hallo welt').entry?.duration.seconds).toBe(
      7200,
    ));
  it('Feste Dauer in Stunden mit Kommentar - kommentar', () =>
    expect(RecordEntryParser.parse('2 stunden kommentar hallo welt').entry?.comment).toBe(
      'hallo welt',
    ));

  it('Fehler, Dauer nur in Kommentar - zeit', () =>
    expect(
      RecordEntryParser.parse('bla fasel kommentar hallo 2 stunden welt').success,
    ).toBeFalse());

  it('Feste Dauer in Stunden ohne Kommentar - zeit', () =>
    expect(RecordEntryParser.parse('2 stunden').entry?.duration.seconds).toBe(7200));
  it('Feste Dauer in Stunden ohne Kommentar - kommentar', () =>
    expect(RecordEntryParser.parse('2 stunden').entry?.comment).toBe(''));
  it('Feste Dauer in Minuten - zeit', () =>
    expect(
      RecordEntryParser.parse('buche 20 minuten kommentar stunden').entry?.duration.seconds,
    ).toBe(1200));
  it('Feste Dauer in Minuten - kommentar', () =>
    expect(RecordEntryParser.parse('buche 20 minuten kommentar stunden').entry?.comment).toBe(
      'stunden',
    ));

  it('Feste Dauer in Stunden und Minuten ohne Kommentar - zeit', () =>
    expect(RecordEntryParser.parse('2 stunden 15 minuten').entry?.duration.seconds).toBe(8100));
  it('Feste Dauer in Stunden und Minuten ohne Kommentar - kommentar', () =>
    expect(RecordEntryParser.parse('2 stunden 15 minuten').entry?.comment).toBe(''));

  it('Feste Dauer in Stunden und Minuten mit Kommentar - zeit', () =>
    expect(
      RecordEntryParser.parse('2 stunden 15 minuten kommentar zu lange').entry?.duration.seconds,
    ).toBe(8100));
  it('Feste Dauer in Stunden und Minuten mit Kommentar - kommentar zu lange', () =>
    expect(RecordEntryParser.parse('2 stunden 15 minuten kommentar zu lange').entry?.comment).toBe(
      'zu lange',
    ));

  it('Von Bis ohne Kommentar - zeit', () =>
    expect(RecordEntryParser.parse('von 8 uhr 30 bis 9 uhr 45').entry?.duration.seconds).toBe(
      4500,
    ));

  it('Von Bis ohne minuten', () =>
    expect(RecordEntryParser.parse('von 8 uhr bis 9 uhr').entry?.duration.seconds).toBe(3600));
  it('Von Bis ohne minuten, zeittexte', () =>
    expect(RecordEntryParser.parse('von acht uhr bis neun uhr').entry?.duration.seconds).toBe(
      3600,
    ));

  it('Von Bis mit  minuten von ', () =>
    expect(RecordEntryParser.parse('von 8 uhr 30 bis 9 uhr').entry?.duration.seconds).toBe(1800));

  it('Von Bis mit  minuten bis ', () =>
    expect(RecordEntryParser.parse('von 8 uhr bis 9 uhr 30').entry?.duration.seconds).toBe(5400));

  it('Von Bis mit Kommentar - zeit', () =>
    expect(
      RecordEntryParser.parse('von 8:30 uhr bis 9:45 uhr kommentar bis pause').entry?.duration
        .seconds,
    ).toBe(4500));
  it('Von Bis mit Kommentar - zeit', () =>
    expect(
      RecordEntryParser.parse(
        'von 8:30 uhr bis 9:45 uhr kommentar bis pause',
      ).entry?.begin?.toString(),
    ).toBe('8:30'));
  it('Von Bis ohne Kommentar - kommentar', () =>
    expect(
      RecordEntryParser.parse('von 8:30 uhr bis 9:45 uhr kommentar bis pause').entry?.comment,
    ).toBe('bis pause'));
});

describe('RecordEntryParser helper functions', () => {
  it('1. Zeit Anfang', () =>
    expect(RecordEntryParser.findFirstTimeString('9:30 ist jetzt')).toBe('9:30'));
  it('1. Zeit Mitte', () =>
    expect(RecordEntryParser.findFirstTimeString('hallo, 9:30 ist jetzt')).toBe('9:30'));
  it('1. Zeit Ende', () =>
    expect(RecordEntryParser.findFirstTimeString('jetzt ist 9:30')).toBe('9:30'));
  it('1. Keine Zeit', () =>
    expect(RecordEntryParser.findFirstTimeString('jetzt ist 123')).toBeFalsy());
  it('1. Leere Zeit', () => expect(RecordEntryParser.findFirstTimeString('')).toBeFalsy());

  it('1. Zeit Anfang .', () =>
    expect(RecordEntryParser.findFirstTimeString('9.30 ist jetzt')).toBe('9:30'));
  it('1. Zeit Mitte .', () =>
    expect(RecordEntryParser.findFirstTimeString('hallo, 9.30 ist jetzt')).toBe('9:30'));
  it('1. Zeit Ende .', () =>
    expect(RecordEntryParser.findFirstTimeString('jetzt ist 9.30')).toBe('9:30'));
  it('1. Keine Zeit .', () =>
    expect(RecordEntryParser.findFirstTimeString('jetzt ist 123')).toBeFalsy());
  it('1. Leere Zeit .', () => expect(RecordEntryParser.findFirstTimeString('')).toBeFalsy());

  it('2. Zeit', () =>
    expect(RecordEntryParser.findSecondTimeString('von 9:30 uhr bis 10:30 uhr')).toBe('10:30'));

  it('Kommentar', () =>
    expect(RecordEntryParser.getComment('hallo kommentar jetzt ist 9:30')).toBe('jetzt ist 9:30'));
  it('Kommentar', () =>
    expect(RecordEntryParser.getComment('hallo Kommentar jetzt Ist 9:30')).toBe('jetzt Ist 9:30'));
  it('kein Kommentar', () =>
    expect(RecordEntryParser.getComment('hallo asd jetzt ist 9:30')).toBe(''));
  it('kein Kommentar2', () =>
    expect(RecordEntryParser.getComment('hallo kommentar2 jetzt ist 9:30')).toBe(''));
  it('leerer Kommentar', () => expect(RecordEntryParser.getComment('')).toBeFalsy());

  it('String ohne Kommentar', () =>
    expect(RecordEntryParser.getStringBeforeComment('hallo kommentar jetzt Ist 9:30')).toBe(
      'hallo',
    ));
  it('String ohne Kommentar', () =>
    expect(RecordEntryParser.getStringBeforeComment('kommentar jetzt ist 9:30')).toBe(''));
  it('String ohne Kommentar', () => expect(RecordEntryParser.getStringBeforeComment('')).toBe(''));
  it('String ohne Kommentar', () =>
    expect(RecordEntryParser.getStringBeforeComment('hallo velo kommentar')).toBe('hallo velo'));

  it('9:30 ist jetzt', () =>
    expect(RecordEntryParser.parse('9:30 ist jetzt').entry?.comment).toBeFalsy());
});
