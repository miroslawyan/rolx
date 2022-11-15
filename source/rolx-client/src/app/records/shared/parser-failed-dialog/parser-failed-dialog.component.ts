import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ParserResult } from '@app/records/core/voice/parser-result';

@Component({
  selector: 'rolx-parser-failed-dialog',
  templateUrl: './parser-failed-dialog.component.html',
  styleUrls: ['./parser-failed-dialog.component.scss'],
})
export class ParserFailedDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: ParserResult) {}
}
