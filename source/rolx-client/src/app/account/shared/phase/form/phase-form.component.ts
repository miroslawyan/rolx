import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Phase, Project, ProjectService } from '@app/account/core';
import { ErrorResponse, ErrorService } from '@app/core/error';

@Component({
  selector: 'rolx-phase-form',
  templateUrl: './phase-form.component.html',
  styleUrls: ['./phase-form.component.scss'],
})
export class PhaseFormComponent implements OnInit {

  @Input() project: Project;
  @Input() phase: Phase;

  phaseForm = this.fb.group({
    name: ['', Validators.required],
    startDate: ['', Validators.required],
    endDate: [''],
    budgetHours: [null, Validators.min(0) ],
    isBillable: [false],
  });

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private projectService: ProjectService,
    private errorService: ErrorService,
    ) { }

  ngOnInit() {
    this.phaseForm.patchValue(this.phase);
  }

  hasError(controlName: string, errorName: string) {
    return this.phaseForm.controls[controlName].hasError(errorName);
  }

  submit() {
    Object.assign(this.phase, this.phaseForm.value);

    this.projectService.update(this.project)
      .subscribe(() => this.cancel(), err => this.handleError(err));
  }

  cancel() {
    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(['/project', this.project.id]);
  }

  private handleError(errorResponse: ErrorResponse) {
    if (!errorResponse.tryToHandleWith(this.phaseForm)) {
      this.errorService.notifyGeneralError();
    }
  }

}
