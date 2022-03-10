import { Component, Inject, Input, LOCALE_ID, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorResponse } from '@app/core/error/error-response';
import { ErrorService } from '@app/core/error/error.service';
import { Duration } from '@app/core/util/duration';
import { assertDefined } from '@app/core/util/utils';
import { Activity } from '@app/projects/core/activity';
import { Billability } from '@app/projects/core/billability';
import { BillabilityService } from '@app/projects/core/billability.service';
import { Subproject } from '@app/projects/core/subproject';
import { SubprojectService } from '@app/projects/core/subproject.service';

@Component({
  selector: 'rolx-activity-form',
  templateUrl: './activity-form.component.html',
  styleUrls: ['./activity-form.component.scss'],
})
export class ActivityFormComponent implements OnInit {
  @Input() subproject!: Subproject;
  @Input() activity!: Activity;

  form = this.fb.group({
    number: ['', [Validators.required, Validators.min(1), Validators.max(99)]],
    name: ['', Validators.required],
    startDate: ['', Validators.required],
    endDate: [''],
    budget: [null, Validators.min(0)],
    billability: [null],
  });

  billabilities: Billability[] = [];

  constructor(
    private readonly router: Router,
    private readonly fb: FormBuilder,
    private readonly subprojectService: SubprojectService,
    private readonly billabilityService: BillabilityService,
    private readonly errorService: ErrorService,
    @Inject(LOCALE_ID) private readonly locale: string,
  ) {}

  ngOnInit() {
    assertDefined(this, 'subproject');
    assertDefined(this, 'activity');

    this.form.patchValue(this.activity);
    this.formBudget = this.activity.budget;
    this.billabilities = this.activity.billability != null ? [this.activity.billability] : [];

    this.billabilityService.getAll().subscribe((billabilities) => {
      if (
        this.activity.billability != null &&
        !billabilities.some((b) => b.id === this.activity.billability?.id)
      ) {
        billabilities = [this.activity.billability, ...billabilities];
      }

      this.billabilities = billabilities;
    });
  }

  get isNew() {
    return this.activity.id == null;
  }

  get formBudget(): Duration {
    const budget = Number.parseFloat(this.form.controls['budget'].value);
    return !Number.isNaN(budget) ? Duration.fromHours(budget) : Duration.Zero;
  }

  set formBudget(value: Duration) {
    const formValue =
      value && !value.isZero
        ? value.hours.toLocaleString(this.locale, { maximumFractionDigits: 1, useGrouping: false })
        : null;
    this.form.controls['budget'].setValue(formValue);
  }

  hasError(controlName: string, errorName: string | string[]) {
    if (Array.isArray(errorName)) {
      return errorName.some((e) => this.form.controls[controlName].hasError(e));
    }

    return this.form.controls[controlName].hasError(errorName);
  }

  submit() {
    Object.assign(this.activity, this.form.value);
    this.activity.budget = this.formBudget;

    this.subprojectService.update(this.subproject).subscribe({
      next: () => this.cancel(),
      error: (err) => this.handleError(err),
    });
  }

  cancel() {
    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(['/subproject', this.subproject.id]);
  }

  compareById(option: Billability, value?: Billability) {
    return option.id === value?.id;
  }

  private handleError(errorResponse: ErrorResponse) {
    if (!errorResponse.tryToHandleWith(this.form)) {
      this.errorService.notifyGeneralError();
    }
  }
}
