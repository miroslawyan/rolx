import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { Activity } from '@app/projects/core/activity';
import { ActivityService } from '@app/projects/core/activity.service';
import * as moment from 'moment';
import { BehaviorSubject, combineLatest, Observable, Subscription } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-activity-selector',
  templateUrl: './activity-selector.component.html',
  styleUrls: ['./activity-selector.component.scss'],
})
export class ActivitySelectorComponent implements OnInit, OnDestroy {
  private readonly subscription = new Subscription();
  private readonly begin$ = new BehaviorSubject<moment.Moment>(moment('1900-01-01'));
  private readonly allActivities$ = new BehaviorSubject<Activity[]>([]);
  private readonly filterText$ = new BehaviorSubject<string>('');

  private activityShadow = new Activity();

  @ViewChild('input') input?: ElementRef;

  @Output()
  selected = new EventEmitter<Activity>();

  @Input()
  excluded: Activity[] = [];

  @Input()
  end = moment('2999-12-31');

  readonly candidates$: Observable<Activity[]> = combineLatest([
    this.allActivities$,
    this.filterText$,
  ]).pipe(map(([activities, filterText]) => this.filterByEndAndFullName(activities, filterText)));

  constructor(private activityService: ActivityService) {}

  @Input()
  get begin(): moment.Moment {
    return this.begin$.value;
  }
  set begin(value: moment.Moment) {
    this.begin$.next(value);
  }

  get activity() {
    return this.activityShadow;
  }

  set activity(value: any) {
    if (!(value instanceof Activity)) {
      this.filterText$.next(value);
      value = this.allActivities$.value.find((a) => a.fullNumber === value);
    }

    if (value) {
      this.activityShadow = value;
      this.selected.emit(value);
    }
  }

  ngOnInit() {
    const excludedIds = new Set(this.excluded.map((ph) => ph.id));
    this.subscription.add(
      this.begin$
        .pipe(
          switchMap((b) => this.activityService.getAll(b)),
          map((as) => as.filter((a) => !excludedIds.has(a.id))),
          map((as) => as.sort((a, b) => a.fullName.localeCompare(b.fullName))),
        )
        .subscribe(this.allActivities$),
    );

    setTimeout(() => this.input?.nativeElement?.focus(), 200);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  fullNameOf(activity?: Activity): string {
    return activity?.fullName ?? '';
  }

  private filterByEndAndFullName(activities: Activity[], filterText: string): Activity[] {
    filterText = filterText.toLocaleLowerCase();
    return activities.filter(
      (a) => a.startDate <= this.end && a.fullName.toLocaleLowerCase().includes(filterText),
    );
  }
}
