import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor(private snackBar: MatSnackBar) { }

  notifyGeneralError() {
    this.snackBar.open(
      'Hoppla, da ist etwas schief gelaufen. Versuch es noch einmal.',
      null,
      { duration: 5000 });
  }
}
