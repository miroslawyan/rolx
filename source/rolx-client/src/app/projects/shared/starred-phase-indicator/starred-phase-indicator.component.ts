import { Component, Input, OnInit } from '@angular/core';
import { assertDefined } from '@app/core/util/utils';
import { FavouritePhaseService } from '@app/projects/core/favourite-phase.service';
import { Phase } from '@app/projects/core/phase';

@Component({
  selector: 'rolx-starred-phase-indicator',
  templateUrl: './starred-phase-indicator.component.html',
  styleUrls: ['./starred-phase-indicator.component.scss'],
})
export class StarredPhaseIndicatorComponent implements OnInit {
  @Input()
  phase!: Phase;

  constructor(private favouritePhaseService: FavouritePhaseService) {}

  ngOnInit(): void {
    assertDefined(this, 'phase');
  }

  toggle() {
    const request = this.isFavourite
      ? this.favouritePhaseService.remove(this.phase)
      : this.favouritePhaseService.add(this.phase);

    request.subscribe();
  }

  get isFavourite(): boolean {
    return this.favouritePhaseService.isFavourite(this.phase);
  }
}
