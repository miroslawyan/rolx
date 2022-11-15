// noinspection JSNonASCIINames
export class RecordEntryParserUtils {
  static positionOfDistinctWord(text: string | undefined, word: string) {
    if (!text || !word) {
      return -1;
    }
    const regex = new RegExp('\\b' + word + '\\b', 'i');
    return text.search(regex);
  }

  static hasDistinctWord(text: string | undefined, word: string) {
    return this.positionOfDistinctWord(text, word) >= 0;
  }

  static hasOneDistinctWordOfList(text: string | undefined, words: string[]) {
    if (!text || !words || words.length === 0) {
      return false;
    }
    return words.map((word) => this.hasDistinctWord(text, word)).some((result) => result);
  }

  static positionOfFirstDistinctWordOfList(text: string | undefined, words: string[]) {
    if (!text || !words || words.length === 0) {
      return -1;
    }
    let firstPos = -1;
    words.forEach((word) => {
      const pos = this.positionOfDistinctWord(text, word);
      if (pos >= 0 && firstPos === -1) {
        firstPos = pos;
      }
    });
    return firstPos;
  }

  static hasWord(text: string | undefined, word: string) {
    if (!text) {
      return false;
    }
    const regex = new RegExp(word);
    return text.toLowerCase().match(regex) !== null;
  }

  static hasOneWordOfList(text: string | undefined, words: string[]) {
    if (!text) {
      return false;
    }
    let result = false;
    words.forEach((word) => {
      result = result || this.hasWord(text, word);
    });
    return result;
  }

  static fixTime(text: string) {
    text = text.replace('.', ':');
    return text;
  }

  static findTimeStrings(text: string) {
    const regex = new RegExp('(2[0-3]|[01]?[0-9])[:|.]([0-5]?[0-9])', 'g');
    const matches = text.match(regex);
    matches?.forEach((s, index) => (matches[index] = this.fixTime(s)));
    return matches;
  }

  static getStringAfterWord(text: string, word: string) {
    const pos = this.positionOfDistinctWord(text, word);
    if (pos < 0) {
      return '';
    }
    return text.slice(pos + word.length).trim();
  }

  static getStringAfterFirstWordOfList(text: string, words: string[]) {
    let result = '';
    words.forEach((word) => {
      const s = this.getStringAfterWord(text, word);
      if (s !== '' && result === '') {
        result = s;
      }
    });
    return result;
  }

  static getStringBeforeWord(text: string, word: string) {
    const pos = this.positionOfDistinctWord(text, word);
    if (pos < 0) {
      return '';
    }
    return text.slice(0, pos).trim();
  }

  static getStringBeforeFirstWordOfList(text: string, words: string[]) {
    let result = '';
    words.forEach((word) => {
      const s = this.getStringBeforeWord(text, word);
      if (s !== '' && result === '') {
        result = s;
      }
    });
    return result;
  }

  static findNthNumber(text: string, index: number): number | undefined {
    const regex = new RegExp('(\\d{1,2})', 'g');
    const result = text.match(regex);
    if (result == null) {
      return undefined;
    }
    if (index >= result.length) {
      return undefined;
    }
    return Number.parseInt(result[index].trim(), 10);
  }

  static replaceWords(text: string, numbers: { [id: string]: string }): string {
    Object.entries(numbers).forEach((entry) => {
      const regex = new RegExp('\\b' + entry[0] + '\\b', 'gi');
      text = text.replace(regex, entry[1]);
    });
    return text;
  }

  static replaceWrittenNumbers(text: string): string {
    const numbers_1_10: { [id: string]: string } = {
      null: '0',
      eins: '1',
      ein: '1',
      eine: '1',
      zwei: '2',
      drei: '3',
      vier: '4',
      fünf: '5',
      sechs: '6',
      sieben: '7',
      acht: '8',
      neun: '9',
    };
    const numbers_10_19: { [id: string]: string } = {
      zehn: '10',
      elf: '11',
      zwölf: '12',
      dreizehn: '13',
      vierzehn: '14',
      fünfzehn: '15',
      sechzehn: '16',
      siebzehn: '17',
      achzehn: '18',
      neunzehn: '19',
    };
    const numbers_20_50: { [id: string]: string } = {
      zwanzig: '2',
      dreissig: '3',
      drei砸ig: '3',
      vierzig: '4',
      fünfzig: '5',
    };

    text = this.replaceWords(text, numbers_1_10);
    text = this.replaceWords(text, numbers_10_19);

    Object.entries(numbers_20_50).forEach((ten_entry) => {
      Object.entries(numbers_1_10).forEach((one_entry) => {
        const regex = new RegExp('\\b' + one_entry[0] + 'und' + ten_entry[0] + '\\b', 'gi');
        text = text.replace(regex, ten_entry[1] + one_entry[1]);
      });
    });
    Object.entries(numbers_20_50).forEach((entry) => {
      const regex = new RegExp('\\b' + entry[0] + '\\b', 'gi');
      text = text.replace(regex, entry[1] + '0');
    });

    return text;
  }
}
