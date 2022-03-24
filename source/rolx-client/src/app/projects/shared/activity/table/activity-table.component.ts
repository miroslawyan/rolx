import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from '@app/auth/core/auth.service';
import { Role } from '@app/auth/core/role';
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
    ...(this.authService.currentApprovalOrError.user.role >= Role.Supervisor ? ['tools'] : []),
  ];

  constructor(private readonly authService: AuthService) {}

  ngOnInit(): void {
    assertDefined(this, 'activities');
  }
}
