import { Component } from '@angular/core';
import { AuthService } from '@app/auth/core/auth.service';

@Component({
  selector: 'rolx-subproject-list-page',
  templateUrl: './subproject-list-page.component.html',
  styleUrls: ['./subproject-list-page.component.scss'],
})
export class SubprojectListPageComponent {
  readonly mayAdd = this.authService.currentApprovalOrError.isSupervisor;

  constructor(private readonly authService: AuthService) {}
}
