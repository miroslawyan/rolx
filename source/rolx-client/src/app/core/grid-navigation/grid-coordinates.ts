export class GridCoordinates {

  constructor(public readonly column, public readonly row) {
  }

  up(): GridCoordinates {
    return new GridCoordinates(this.column, this.row - 1);
  }

  down(): GridCoordinates {
    return new GridCoordinates(this.column, this.row + 1);
  }

  left(): GridCoordinates {
    return new GridCoordinates(this.column - 1, this.row);
  }

  right(): GridCoordinates {
    return new GridCoordinates(this.column + 1, this.row);
  }

  isSame(other: GridCoordinates): boolean {
    return this.column === other.column && this.row === other.row;
  }

}
