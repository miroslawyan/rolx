import { Component, Input, OnInit } from '@angular/core';
import { assertDefined } from '@app/core/util/utils';
import { Activity } from '@app/projects/core/activity';
import { FavouriteActivityService } from '@app/projects/core/favourite-activity.service';

@Component({
  selector: 'rolx-starred-activity-indicator',
  templateUrl: './starred-activity-indicator.component.html',
  styleUrls: ['./starred-activity-indicator.component.scss'],
})
export class StarredActivityIndicatorComponent implements OnInit {
  @Input()
  activity!: Activity;

  constructor(private favouriteActivityService: FavouriteActivityService) {}

  ngOnInit(): void {
    assertDefined(this, 'activity');
  }

  toggle() {
    const request = this.isFavourite
      ? this.favouriteActivityService.remove(this.activity)
      : this.favouriteActivityService.add(this.activity);

    request.subscribe();
  }

  get isFavourite(): boolean {
    return this.favouriteActivityService.isFavourite(this.activity);
  }
}
