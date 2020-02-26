import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

export interface ReasonDialogData {
  reason: string | null;
}

@Component({
  selector: 'rolx-reason-dialog',
  templateUrl: './reason-dialog.component.html',
  styleUrls: ['./reason-dialog.component.scss'],
})
export class ReasonDialogComponent {

  readonly reason = new FormControl();
  readonly form = new FormGroup({ reason: this.reason});

  constructor(@Inject(MAT_DIALOG_DATA) public data: ReasonDialogData,
              private dialogRef: MatDialogRef<ReasonDialogComponent>) {
    this.reason.setValue(data.reason);
  }

  submit() {
    this.dialogRef.close(this.reason.value);
  }

}
