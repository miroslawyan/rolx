import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Role } from '@app/auth/core';
import { ErrorResponse, ErrorService } from '@app/core/error';
import { User, UserService } from '@app/user-management/core';

@Component({
  selector: 'rolx-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.scss'],
})
export class UserFormComponent implements OnInit {
  readonly Role = Role;

  readonly roleControl = new FormControl(null, Validators.required);
  readonly form = new FormGroup({
    role: this.roleControl,
  });

  @Input()
  user: User;

  constructor(private router: Router,
              private userService: UserService,
              private errorService: ErrorService) { }

  ngOnInit() {
    this.roleControl.setValue(this.user.role);
  }

  submit() {
    Object.assign(this.user, this.form.value);

    this.userService.update(this.user)
      .subscribe(() => this.cancel(), err => this.handleError(err));
  }

  cancel() {
    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(['user']);
  }

  private handleError(errorResponse: ErrorResponse) {
    if (!errorResponse.tryToHandleWith(this.form)) {
      this.errorService.notifyGeneralError();
    }
  }

}
