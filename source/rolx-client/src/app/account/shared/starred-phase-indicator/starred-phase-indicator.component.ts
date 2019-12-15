import { Component, Input } from '@angular/core';
import { FavouritePhaseService, Phase } from '@app/account/core';

@Component({
  selector: 'rolx-starred-phase-indicator',
  templateUrl: './starred-phase-indicator.component.html',
  styleUrls: ['./starred-phase-indicator.component.scss'],
})
export class StarredPhaseIndicatorComponent {

  @Input()
  phase: Phase;

  constructor(private favouritePhaseService: FavouritePhaseService) { }

  toggle() {
    const request = this.isFavourite ?
      this.favouritePhaseService.remove(this.phase) :
      this.favouritePhaseService.add(this.phase);

    request.subscribe();
  }

  get isFavourite(): boolean {
    return this.favouritePhaseService.isFavourite(this.phase);
  }
}
