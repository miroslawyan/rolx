import { Injectable, NgZone } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root',
})
export class VoiceService {
  private readonly recognition?: SpeechRecognition;

  get isSupported() {
    return this.recognition != null;
  }

  constructor(private readonly ngZone: NgZone, private _snackBar: MatSnackBar) {
    console.log('--- VoiceService.ctor()');
    try {
      this.recognition = new webkitSpeechRecognition();
      this.recognition.lang = 'de-CH';
      this.recognition.continuous = true;
      this.recognition.interimResults = false;
    } catch (e) {
      this.recognition = undefined;
      console.log('--- VoiceService.ctor() - Failed to initialize webkitSpeechRecognition', e);
    }
  }

  async recognizeText(): Promise<string> {
    if (!this.isSupported) {
      return '';
    }

    this.recognition?.start();
    this._snackBar
      .open('RolXa hört zu...', 'Stop', { duration: 10 * 1000 })
      .afterDismissed()
      .subscribe(() => this.recognition?.stop());

    const event = await new Promise<SpeechRecognitionEvent>((resolve) => {
      if (this.recognition) {
        this.recognition.onresult = (e) => this.ngZone.run(() => resolve(e));
      }
    });

    const result = event.results[0][0].transcript.toString();
    this._snackBar.open(`Du sagtest: '${result}'`, 'Ok', { duration: 5 * 1000 });

    return result;
  }
}
