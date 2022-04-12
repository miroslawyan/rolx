import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorResponse } from '@app/core/error/error-response';
import { ErrorService } from '@app/core/error/error.service';
import { assertDefined } from '@app/core/util/utils';
import { Role } from '@app/users/core/role';
import { User } from '@app/users/core/user';
import { UserService } from '@app/users/core/user.service';

@Component({
  selector: 'rolx-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.scss'],
})
export class UserFormComponent implements OnInit {
  readonly Role = Role;

  readonly roleControl = new FormControl(null, Validators.required);
  readonly entryDateControl = new FormControl(null, Validators.required);
  readonly leavingDateControl = new FormControl({ value: null });
  readonly form = new FormGroup({
    role: this.roleControl,
    entryDate: this.entryDateControl,
    leavingDate: this.leavingDateControl,
  });

  @Input()
  user!: User;

  constructor(
    private router: Router,
    private userService: UserService,
    private errorService: ErrorService,
  ) {}

  ngOnInit() {
    assertDefined(this, 'user');

    this.form.patchValue(this.user);

    // somehow patchValue doesn't use the properties with getter
    // lets do it explicitly
    this.leavingDateControl.setValue(this.user.leavingDate);
  }

  submit() {
    Object.assign(this.user, this.form.value);

    this.userService.update(this.user).subscribe({
      next: () => this.cancel(),
      error: (err) => this.handleError(err),
    });
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
