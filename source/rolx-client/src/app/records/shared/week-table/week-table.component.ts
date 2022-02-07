import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { assertDefined } from '@app/core/util/utils';
import { FavouritePhaseService } from '@app/projects/core/favourite-phase.service';
import { Phase } from '@app/projects/core/phase';
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
  private inputPhases: Phase[] = [];
  private favouritePhases: Phase[] = [];
  private homegrownPhases: Phase[] = [];
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

  readonly displayedColumns: string[] = ['phase', ...this.weekdays];

  @Input()
  records: Record[] = [];

  @Input()
  user!: User;

  allPhases: (Phase | null)[] = [];

  isAddingPhase = false;

  constructor(
    private favouritePhaseService: FavouritePhaseService,
    private workRecordService: WorkRecordService,
  ) {}

  @Input()
  get phases(): Phase[] {
    return this.inputPhases;
  }
  set phases(value: Phase[]) {
    this.inputPhases = value.filter((ph) => this.records.some((r) => ph.isOpenAt(r.date)));
    this.homegrownPhases = [];
    this.isAddingPhase = false;

    this.update();
  }

  ngOnInit() {
    assertDefined(this, 'user');

    this.subscriptions.add(
      this.favouritePhaseService.favourites$.subscribe((phs) => (this.favourites = phs)),
    );
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  startAdding() {
    this.isAddingPhase = true;
    this.update();
  }

  addHomegrown(phase: Phase) {
    this.isAddingPhase = false;
    this.homegrownPhases.push(phase);
    this.update();
  }

  submit(record: Record, index: number) {
    this.workRecordService.update(record).subscribe((r) => (this.records[index] = r));
  }

  private set favourites(value: Phase[]) {
    this.favouritePhases = value;
    this.update();
  }

  private update() {
    const localPhasesIds = new Set<number>(
      [...this.inputPhases, ...this.homegrownPhases].map((ph) => ph.id),
    );

    const nonLocalFavourites = this.favouritePhases.filter((ph) => !localPhasesIds.has(ph.id));

    const sortedPhases = [...this.inputPhases, ...nonLocalFavourites].sort((a, b) =>
      a.fullName.localeCompare(b.fullName),
    );

    this.allPhases = [...sortedPhases, ...this.homegrownPhases];
    if (this.isAddingPhase) {
      this.allPhases.push(null);
    }
  }
}
