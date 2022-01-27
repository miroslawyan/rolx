import { Component, Input } from '@angular/core';
import { Phase } from '@app/projects/core/phase';

@Component({
  selector: 'rolx-phase-table',
  templateUrl: './phase-table.component.html',
  styleUrls: ['./phase-table.component.scss'],
})
export class PhaseTableComponent {

  @Input() phases: Phase[];

  displayedColumns: string[] = [
    'number',
    'name',
    'startDate',
    'endDate',
    'budgetHours',
    'isBillable',
    'tools',
  ];

}
