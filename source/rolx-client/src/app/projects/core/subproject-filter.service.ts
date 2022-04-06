import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SubprojectFilterService {
  private _filterText = '';

  showClosed = false;

  get filterText() {
    return this._filterText;
  }
  set filterText(value: string) {
    this._filterText = value.trim();
  }
}
