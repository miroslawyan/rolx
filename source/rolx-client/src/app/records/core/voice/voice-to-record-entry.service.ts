import { Injectable } from '@angular/core';
import { VoiceService } from '@app/core/voice/voice.service';

import { ParserResult } from './parser-result';
import { RecordEntryParser } from './record-entry-parser';

@Injectable({
  providedIn: 'root',
})
export class VoiceToRecordEntryService {
  constructor(private readonly voiceService: VoiceService) {}

  async getNext(): Promise<ParserResult> {
    const text = await this.voiceService.recognizeText();

    const result = RecordEntryParser.parse(text);
    console.log(text, result);

    return result;
  }
}
