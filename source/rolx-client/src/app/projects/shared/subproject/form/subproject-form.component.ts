import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorResponse } from '@app/core/error/error-response';
import { ErrorService } from '@app/core/error/error.service';
import { SetupService } from '@app/core/setup/setup.service';
import { assertDefined } from '@app/core/util/utils';
import { Subproject } from '@app/projects/core/subproject';
import { SubprojectService } from '@app/projects/core/subproject.service';

@Component({
  selector: 'rolx-subproject-form',
  templateUrl: './subproject-form.component.html',
  styleUrls: ['./subproject-form.component.scss'],
})
export class SubprojectFormComponent implements OnInit {
  @Input() subproject!: Subproject;

  form = this.fb.group({
    number: [
      '',
      [Validators.required, Validators.pattern(this.setupService.info.projectNumberPattern)],
    ],
    name: ['', Validators.required],
  });

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private subprojectService: SubprojectService,
    private setupService: SetupService,
    private errorService: ErrorService,
  ) {}

  ngOnInit() {
    assertDefined(this, 'subproject');

    this.form.patchValue(this.subproject);
  }

  get isNew() {
    return this.subproject.id == null;
  }

  hasError(controlName: string, errorName: string) {
    return this.form.controls[controlName].hasError(errorName);
  }

  submit() {
    Object.assign(this.subproject, this.form.value);

    const request = this.isNew
      ? this.subprojectService.create(this.subproject)
      : this.subprojectService.update(this.subproject);
    request.subscribe({
      next: (p) => this.done(p),
      error: (err) => this.handleError(err),
    });
  }

  done(subproject: Subproject) {
    const target = ['subproject', subproject.id];
    if (this.isNew) {
      target.push('activity');
      target.push('new');
    }

    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(target);
  }

  cancel() {
    const target: (number | string)[] = ['subproject'];
    if (!this.isNew) {
      target.push(this.subproject.id);
    }

    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(target);
  }

  private handleError(errorResponse: ErrorResponse) {
    if (!errorResponse.tryToHandleWith(this.form)) {
      this.errorService.notifyGeneralError();
    }
  }
}
