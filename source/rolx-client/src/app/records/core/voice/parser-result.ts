import { RecordEntry } from '@app/records/core/record-entry';

export class ParserResult {
  entry?: RecordEntry;

  failReason?: string;

  get success() {
    return this.entry != null;
  }

  private constructor(public readonly text: string) {}

  static Success(text: string, entry: RecordEntry) {
    const result = new ParserResult(text);
    result.entry = entry;
    return result;
  }

  static Fail(text: string, reason: string) {
    const result = new ParserResult(text);
    result.failReason = reason;
    return result;
  }
}
