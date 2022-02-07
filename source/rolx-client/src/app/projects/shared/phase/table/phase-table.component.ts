import { Component, Input, OnInit } from '@angular/core';
import { assertDefined } from '@app/core/util/utils';
import { Phase } from '@app/projects/core/phase';

@Component({
  selector: 'rolx-phase-table',
  templateUrl: './phase-table.component.html',
  styleUrls: ['./phase-table.component.scss'],
})
export class PhaseTableComponent implements OnInit {
  @Input() phases!: Phase[];

  displayedColumns: string[] = [
    'number',
    'name',
    'startDate',
    'endDate',
    'budgetHours',
    'isBillable',
    'tools',
  ];

  ngOnInit(): void {
    assertDefined(this, 'phases');
  }
}
