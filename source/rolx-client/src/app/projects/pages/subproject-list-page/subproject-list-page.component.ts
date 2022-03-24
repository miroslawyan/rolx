import { Component } from '@angular/core';
import { AuthService } from '@app/auth/core/auth.service';
import { Role } from '@app/auth/core/role';

@Component({
  selector: 'rolx-subproject-list-page',
  templateUrl: './subproject-list-page.component.html',
  styleUrls: ['./subproject-list-page.component.scss'],
})
export class SubprojectListPageComponent {
  readonly mayAdd = this.authService.currentApprovalOrError.user.role >= Role.Supervisor;

  constructor(private readonly authService: AuthService) {}
}
