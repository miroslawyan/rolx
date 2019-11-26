import { Component, Input, OnInit } from '@angular/core';
import { Phase } from '@app/account/core';

@Component({
  selector: 'rolx-phase-table',
  templateUrl: './phase-table.component.html',
  styleUrls: ['./phase-table.component.scss'],
})
export class PhaseTableComponent implements OnInit {

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

  constructor() { }

  ngOnInit() {
  }

}
