import { Duration } from '@app/core/util/duration';
import { TimeOfDay } from '@app/core/util/time-of-day';
import { RecordEntry } from '@app/records/core/record-entry';

import { ParserResult } from './parser-result';
import { RecordEntryParserUtils } from './record-entry-parser-utils';

class parserState {
  entry: RecordEntry = new RecordEntry();
  success = false;
  errorMsg = '';
}

export class RecordEntryParser {
  static language = {
    oclock: ['uhr'],
    hour: ['stunde', 'stunden'],
    minute: ['minute', 'minuten'],
    comment: ['kommentar', 'kommentare', 'kommentaren'],
    until: ['bis'],
  };

  static getComment(text: string) {
    return RecordEntryParserUtils.getStringAfterFirstWordOfList(text, this.language.comment);
  }

  static getStringBeforeComment(text: string): string {
    return RecordEntryParserUtils.getStringBeforeFirstWordOfList(text, this.language.comment);
  }

  static findFirstTimeString(text: string) {
    const result = RecordEntryParserUtils.findTimeStrings(text);
    return result == null ? undefined : result[0];
  }

  static findSecondTimeString(text: string) {
    const result = RecordEntryParserUtils.findTimeStrings(text);
    return result == null ? undefined : result[1];
  }

  static parseFromTo(text: string, entry: RecordEntry): boolean {
    const von = RecordEntryParserUtils.getStringBeforeFirstWordOfList(text, this.language.until);
    const h1 = RecordEntryParserUtils.findNthNumber(von, 0);
    let m1 = RecordEntryParserUtils.findNthNumber(von, 1);
    m1 = m1 === undefined ? 0 : m1;

    const bis = RecordEntryParserUtils.getStringAfterFirstWordOfList(text, this.language.until);
    const h2 = RecordEntryParserUtils.findNthNumber(bis, 0);
    let m2 = RecordEntryParserUtils.findNthNumber(bis, 1);
    m2 = m2 === undefined ? 0 : m2;

    //console.log(text+' -> '+von+' - '+bis+' '+h1+':'+m1+'   -    '+h2+':'+m1);
    if (h1 === undefined || h2 === undefined) {
      return false;
    }

    const timeFrom = TimeOfDay.fromHours(h1 + m1 / 60.0);
    const timeTo = TimeOfDay.fromHours(h2 + m2 / 60.0);

    entry.begin = timeFrom;
    entry.duration = timeTo.sub(timeFrom);
    return true;
  }

  static parseDuration(text: string, entry: RecordEntry): boolean {
    const hasHours = RecordEntryParserUtils.hasOneWordOfList(text, this.language.hour);

    let hours: number | undefined;
    let minutes: number | undefined;
    let foundTime = false;

    if (hasHours) {
      hours = RecordEntryParserUtils.findNthNumber(text, 0);
      const number1 = RecordEntryParserUtils.findNthNumber(text, 1);
      minutes = number1 ?? 0;
      if (hours !== undefined) {
        foundTime = true;
      }
    } else {
      minutes = RecordEntryParserUtils.findNthNumber(text, 0);
      if (minutes !== undefined) {
        foundTime = true;
      }
    }

    if (foundTime) {
      minutes = minutes ?? 0;
      hours = hours ?? 0;
      entry.duration = Duration.fromHours(hours + minutes / 60.0);
      return true;
    }

    return false;
  }

  static parseComment(text: string, state: parserState) {
    if (RecordEntryParserUtils.hasOneDistinctWordOfList(text, this.language.comment)) {
      state.entry.comment = this.getComment(text);
      text = this.getStringBeforeComment(text);
    } else {
      state.entry.comment = '';
    }
    return text;
  }

  static ParseFromToSuccessful(text: string, state: parserState) {
    if (RecordEntryParserUtils.hasOneDistinctWordOfList(text, this.language.until)) {
      if (this.parseFromTo(text, state.entry)) {
        return true;
      }
      state.success = false;
      state.errorMsg = 'Does not seem to include two time specifications like "8:15"';
    }
    return false;
  }

  static ParseDurationSuccessful(text: string, state: parserState) {
    if (
      RecordEntryParserUtils.hasOneWordOfList(text, this.language.hour.concat(this.language.minute))
    ) {
      if (this.parseDuration(text, state.entry)) {
        return true;
      }
      state.success = false;
      state.errorMsg = 'Does not seem to include a duration like "8 Stunden 15 Minuten"';
    }
    return false;
  }

  static parse(text: string): ParserResult {
    // Spezifikation
    // - Wenn Text das Wort "Stunde" enthält, gehen wir davon aus, dass im Text eine fixe Zeitdauer vorkommt
    // - Wenn der Text das Wort "Uhr" enthält, gehen wir davon aus, dass von -bis vorkommt
    // - Wenn der Text das Wort "Kommentar" enthält, wird alles danach als Kommentar gespeichert

    const state: parserState = new parserState();

    text = this.parseComment(text, state);
    text = RecordEntryParserUtils.replaceWrittenNumbers(text);

    if (this.ParseFromToSuccessful(text, state)) {
      return ParserResult.Success(text, state.entry);
    }

    if (this.ParseDurationSuccessful(text, state)) {
      return ParserResult.Success(text, state.entry);
    }

    if (state.errorMsg === '') {
      state.errorMsg = 'Keine Zeiten gefunden';
    }

    return ParserResult.Fail(text, state.errorMsg);
  }
}
