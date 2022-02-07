import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Role } from '@app/auth/core/role';
import { ErrorResponse } from '@app/core/error/error-response';
import { ErrorService } from '@app/core/error/error.service';
import { assertDefined } from '@app/core/util/utils';
import { User } from '@app/users/core/user';
import { UserService } from '@app/users/core/user.service';
import * as moment from 'moment';
import { Subscription } from 'rxjs';

@Component({
  selector: 'rolx-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.scss'],
})
export class UserFormComponent implements OnInit, OnDestroy {
  private readonly subscriptions = new Subscription();

  readonly Role = Role;

  readonly roleControl = new FormControl(null, Validators.required);
  readonly entryDateControl = new FormControl(null);
  readonly leavingDateControl = new FormControl({ value: null, disabled: true });
  readonly form = new FormGroup({
    role: this.roleControl,
    entryDate: this.entryDateControl,
    leavingDate: this.leavingDateControl,
  });

  @Input()
  user!: User;

  minLeavingDate?: moment.Moment;

  constructor(
    private router: Router,
    private userService: UserService,
    private errorService: ErrorService,
  ) {
    this.subscriptions.add(
      this.entryDateControl.valueChanges.subscribe((v) => this.updateLeavingDate(v)),
    );
  }

  ngOnInit() {
    assertDefined(this, 'user');

    this.form.patchValue(this.user);

    // somehow patchValue doesn't use the properties with getter
    // lets do it explicitly
    this.leavingDateControl.setValue(this.user.leavingDate);
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  submit() {
    Object.assign(this.user, this.form.value);

    this.userService.update(this.user).subscribe(
      () => this.cancel(),
      (err) => this.handleError(err),
    );
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

  private updateLeavingDate(entryDate: moment.Moment) {
    if (entryDate) {
      this.leavingDateControl.enable();
      this.minLeavingDate = entryDate.clone().add(1, 'day');
    } else {
      this.leavingDateControl.setValue(null);
      this.leavingDateControl.disable();
    }
  }
}
