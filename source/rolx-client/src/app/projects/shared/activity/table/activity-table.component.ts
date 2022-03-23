import { Component, Input, OnInit } from '@angular/core';
import { assertDefined } from '@app/core/util/utils';
import { Activity } from '@app/projects/core/activity';

@Component({
  selector: 'rolx-activity-table',
  templateUrl: './activity-table.component.html',
  styleUrls: ['./activity-table.component.scss'],
})
export class ActivityTableComponent implements OnInit {
  @Input() activities!: Activity[];

  displayedColumns: string[] = [
    'number',
    'name',
    'startDate',
    'endDate',
    'budgetHours',
    'actualHours',
    'isBillable',
    'tools',
  ];

  ngOnInit(): void {
    assertDefined(this, 'activities');
  }
}
