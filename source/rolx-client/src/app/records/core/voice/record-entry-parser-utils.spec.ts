import { RecordEntryParserUtils } from './record-entry-parser-utils';

describe('RecordEntryParserUtils helper functions', () => {
  it('kleinschreibung', () =>
    expect(RecordEntryParserUtils.hasDistinctWord('zwei uhr dreissig', 'uhr')).toBeTrue());
  it('klein und grossschreibung', () =>
    expect(RecordEntryParserUtils.hasDistinctWord('zwei Uhr dreissig gross', 'uhr')).toBeTrue());
  it('substring', () =>
    expect(RecordEntryParserUtils.hasDistinctWord('zwei nuhr dreissig', 'uhr')).toBeFalse());
  it('leere Wortsuche (text leerstring)', () =>
    expect(RecordEntryParserUtils.hasDistinctWord('', 'uhr')).toBeFalse());

  it('find word 1', () =>
    expect(RecordEntryParserUtils.positionOfDistinctWord('zwei uhr dreissig', 'uhr')).toBe(5));
  it('find word 2', () =>
    expect(RecordEntryParserUtils.positionOfDistinctWord('zwei uhr dreissig', 'zwei')).toBe(0));
  it('find word 3', () =>
    expect(RecordEntryParserUtils.positionOfDistinctWord('zwei uhr dreissig', 'uhrs')).toBe(-1));

  it('positionofFirstDistinctWordOfList 1', () =>
    expect(
      RecordEntryParserUtils.positionOfFirstDistinctWordOfList('zwei nuhr dreissig', [
        'uhr',
        'bla',
      ]),
    ).toBe(-1));
  it('positionofFirstDistinctWordOfList 2', () =>
    expect(
      RecordEntryParserUtils.positionOfFirstDistinctWordOfList('zwei uhr dreissig', ['uhr', 'bla']),
    ).toBe(5));
  it('positionofFirstDistinctWordOfList 3', () =>
    expect(
      RecordEntryParserUtils.positionOfFirstDistinctWordOfList('zwei bla dreissig', ['uhr', 'bla']),
    ).toBe(5));
  it('positionofFirstDistinctWordOfList 4', () =>
    expect(RecordEntryParserUtils.positionOfFirstDistinctWordOfList('', ['uhr', 'bla'])).toBe(-1));
  it('positionofFirstDistinctWordOfList 3', () =>
    expect(RecordEntryParserUtils.positionOfFirstDistinctWordOfList('zwei bla dreissig', [])).toBe(
      -1,
    ));

  it('hasDistincWordofList 1', () =>
    expect(
      RecordEntryParserUtils.hasOneDistinctWordOfList('zwei nuhr dreissig', ['uhr', 'bla']),
    ).toBeFalse());
  it('hasDistincWordofList 2', () =>
    expect(
      RecordEntryParserUtils.hasOneDistinctWordOfList('zwei uhr dreissig', ['uhr', 'bla']),
    ).toBeTrue());
  it('hasDistincWordofList 3', () =>
    expect(
      RecordEntryParserUtils.hasOneDistinctWordOfList('zwei bla dreissig', ['uhr', 'bla']),
    ).toBeTrue());

  it('number 1st', () =>
    expect(RecordEntryParserUtils.findNthNumber('hallo 1 jetzt 2 und 12 aber 34', 0)).toBe(1));
  it('number 2nd', () =>
    expect(RecordEntryParserUtils.findNthNumber('hallo 1 jetzt 2 und 12 aber 34', 1)).toBe(2));
  it('number 3rd', () =>
    expect(RecordEntryParserUtils.findNthNumber('hallo 1 jetzt 2 und 12 aber 34', 2)).toBe(12));
  it('number 4rd', () =>
    expect(RecordEntryParserUtils.findNthNumber('hallo 1 jetzt 2 und 12 aber 34', 3)).toBe(34));
  it('number start', () =>
    expect(RecordEntryParserUtils.findNthNumber('11 jetzt 2 und 12 aber 34', 0)).toBe(11));
  it('number oob', () =>
    expect(RecordEntryParserUtils.findNthNumber('hallo 1 jetzt 2 und 12 aber 34', 5)).toBeFalsy());

  it('replace written number null', () =>
    expect(RecordEntryParserUtils.replaceWrittenNumbers('null')).toBe('0'));
  it('replace written number Null', () =>
    expect(RecordEntryParserUtils.replaceWrittenNumbers('Null')).toBe('0'));
  it('replace written number multiple null', () =>
    expect(RecordEntryParserUtils.replaceWrittenNumbers('Null null NULL')).toBe('0 0 0'));
  it('replace written number eins zwei drei', () =>
    expect(RecordEntryParserUtils.replaceWrittenNumbers('eins zwei drei')).toBe('1 2 3'));
  it('replace written number fünfzehn', () =>
    expect(RecordEntryParserUtils.replaceWrittenNumbers('fünfzehn')).toBe('15'));
  it('replace written number zwanzig', () =>
    expect(RecordEntryParserUtils.replaceWrittenNumbers('zwanzig')).toBe('20'));
  it('replace written number zweiundvierzig', () =>
    expect(RecordEntryParserUtils.replaceWrittenNumbers('zweiundvierzig')).toBe('42'));
  it('replace written number dreiunddreissig', () =>
    expect(RecordEntryParserUtils.replaceWrittenNumbers('dreiunddreissig')).toBe('33'));
  it('replace written number dreiunddrei\u{7838}ig', () =>
    expect(RecordEntryParserUtils.replaceWrittenNumbers('dreiunddrei\u{7838}ig')).toBe('33'));

  it('GetStringAfterWord 1', () =>
    expect(RecordEntryParserUtils.getStringAfterWord('hallo Reto wie geht', 'Reto')).toBe(
      'wie geht',
    ));
  it('GetStringAfterWord 2', () =>
    expect(RecordEntryParserUtils.getStringAfterWord('hallo Reto wie geht', '')).toBe(''));
  it('GetStringAfterWord 3', () =>
    expect(RecordEntryParserUtils.getStringAfterWord('', 'Reto')).toBe(''));
  it('GetStringAfterWord 4', () =>
    expect(RecordEntryParserUtils.getStringAfterWord('hallo Reto wie geht', 'geht')).toBe(''));
  it('GetStringAfterWord 5', () =>
    expect(RecordEntryParserUtils.getStringAfterWord('hallo Reto wie geht', 'hallo')).toBe(
      'Reto wie geht',
    ));

  it('GetStringAfterFirsWordOfLoist 1', () =>
    expect(
      RecordEntryParserUtils.getStringAfterFirstWordOfList('hallo Reto wie geht', ['Reto']),
    ).toBe('wie geht'));
});
