import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from '@app/auth/core/auth.service';
import { SortService } from '@app/core/persistence/sort.service';
import { assertDefined } from '@app/core/util/utils';
import { Activity } from '@app/projects/core/activity';

@Component({
  selector: 'rolx-activity-table',
  templateUrl: './activity-table.component.html',
  styleUrls: ['./activity-table.component.scss'],
})
export class ActivityTableComponent implements OnInit {
  private _activities!: Activity[];
  readonly dataSource = new MatTableDataSource<Activity>();

  @Input()
  get activities(): Activity[] {
    return this._activities;
  }
  set activities(value: Activity[]) {
    this._activities = value;
    this.dataSource.data = value;
  }

  displayedColumns: string[] = [
    'number',
    'name',
    'startDate',
    'endDate',
    'budgetHours',
    'actualHours',
    'isBillable',
    ...(this.authService.currentApprovalOrError.isSupervisor ? ['tools'] : []),
  ];

  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  constructor(
    private readonly authService: AuthService,
    private readonly sortService: SortService,
  ) {}

  ngOnInit(): void {
    assertDefined(this, '_activities');
    assertDefined(this, 'sort');

    this.dataSource.sort = this.sort;

    this.sort.sort(
      this.sortService.get('Activity', {
        id: this.displayedColumns[0],
        start: 'asc',
        disableClear: true,
      }),
    );
    this.sort.sortChange.subscribe((sort) => this.sortService.set('Activity', sort));
  }

  tpd(activity: Activity): Activity {
    return activity;
  }
}
