import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorResponse, ErrorService } from '@app/core/error';
import { SetupService } from '@app/core/setup';
import { Project, ProjectService } from '@app/projects/core';

@Component({
  selector: 'rolx-project-form',
  templateUrl: './project-form.component.html',
  styleUrls: ['./project-form.component.scss'],
})
export class ProjectFormComponent implements OnInit {

  @Input() project: Project;

  projectForm = this.fb.group({
    number: ['', [
      Validators.required,
      Validators.pattern(this.setupService.info.projectNumberPattern),
    ]],
    name: ['', Validators.required],
  });

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private projectService: ProjectService,
    private setupService: SetupService,
    private errorService: ErrorService,
  ) { }

  ngOnInit() {
    this.projectForm.patchValue(this.project);
  }

  get isNew() {
    return this.project.id == null;
  }

  hasError(controlName: string, errorName: string) {
    return this.projectForm.controls[controlName].hasError(errorName);
  }

  submit() {
    Object.assign(this.project, this.projectForm.value);

    const request = this.isNew ? this.projectService.create(this.project) : this.projectService.update(this.project);
    request.subscribe(p => this.done(p), err => this.handleError(err));
  }

  done(project: Project) {
    const target = ['project', project.id];
    if (this.isNew) {
      target.push('phase');
      target.push('new');
    }

    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(target);
  }

  cancel() {
    const target: (number|string)[] = ['project'];
    if (!this.isNew) {
      target.push(this.project.id);
    }

    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(target);
  }

  private handleError(errorResponse: ErrorResponse) {
    if (!errorResponse.tryToHandleWith(this.projectForm)) {
      this.errorService.notifyGeneralError();
    }
  }

}
