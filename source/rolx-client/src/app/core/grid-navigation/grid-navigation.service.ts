import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { GridCoordinates } from './grid-coordinates';

@Injectable({
  providedIn: 'root',
})
export class GridNavigationService {

  private readonly coordinatesSubject = new Subject<GridCoordinates>();

  readonly coordinates$ = this.coordinatesSubject.asObservable();

  constructor() { }

  navigateTo(coordinates: GridCoordinates) {
    this.coordinatesSubject.next(coordinates);
  }
}
