import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ErrorService } from '@app/core/error/error.service';
import { assertDefined } from '@app/core/util/utils';
import { Activity } from '@app/projects/core/activity';
import { FavouriteActivityService } from '@app/projects/core/favourite-activity.service';
import { Record } from '@app/records/core/record';
import { WorkRecordService } from '@app/records/core/work-record.service';
import { User } from '@app/users/core/user';
import { Subscription } from 'rxjs';

@Component({
  selector: 'rolx-week-table',
  templateUrl: './week-table.component.html',
  styleUrls: ['./week-table.component.scss'],
})
export class WeekTableComponent implements OnInit, OnDestroy {
  private inputActivities: Activity[] = [];
  private favouriteActivities: Activity[] = [];
  private homegrownActivities: Activity[] = [];
  private subscriptions = new Subscription();

  readonly weekdays = [
    'monday',
    'tuesday',
    'wednesday',
    'thursday',
    'friday',
    'saturday',
    'sunday',
  ];

  readonly displayedColumns: string[] = ['activity', ...this.weekdays];

  @Input()
  records: Record[] = [];

  @Input()
  user!: User;

  @Input()
  isCurrentUser = false;

  rowActivities: (Activity | null)[] = [];
  allActivities: Activity[] = [];

  isAddingActivity = false;

  constructor(
    private favouriteActivityService: FavouriteActivityService,
    private workRecordService: WorkRecordService,
    private errorService: ErrorService,
  ) {}

  @Input()
  get activities(): Activity[] {
    return this.inputActivities;
  }
  set activities(value: Activity[]) {
    this.inputActivities = value.filter((activity) =>
      this.records.some((record) => activity.isOpenAt(record.date)),
    );
    this.homegrownActivities = [];
    this.isAddingActivity = false;

    this.update();
  }

  ngOnInit() {
    assertDefined(this, 'user');

    if (this.isCurrentUser) {
      this.subscriptions.add(
        this.favouriteActivityService.favourites$.subscribe((phs) => (this.favourites = phs)),
      );
    }
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  startAdding() {
    this.isAddingActivity = true;
    this.update();
  }

  addHomegrown(activity: Activity) {
    this.isAddingActivity = false;
    this.homegrownActivities.push(activity);
    this.update();
  }

  submit(record: Record, index: number) {
    this.workRecordService.update(this.user.id, record).subscribe({
      next: (r) => (this.records[index] = r),
      error: (err) => {
        console.error(err);
        this.errorService.notifyGeneralError();
      },
    });
  }

  private set favourites(value: Activity[]) {
    this.favouriteActivities = value;
    this.update();
  }

  private update() {
    const localActivitiesIds = new Set<number>(
      [...this.inputActivities, ...this.homegrownActivities].map((ph) => ph.id),
    );

    const nonLocalFavourites = this.favouriteActivities.filter(
      (ph) => !localActivitiesIds.has(ph.id),
    );

    const sortedActivities = [...this.inputActivities, ...nonLocalFavourites].sort((a, b) =>
      a.fullNumber.localeCompare(b.fullNumber),
    );

    this.allActivities = [...sortedActivities, ...this.homegrownActivities];
    this.rowActivities = this.isAddingActivity ? [...this.allActivities, null] : this.allActivities;
  }
}
