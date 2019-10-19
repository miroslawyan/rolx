import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Project, ProjectService } from '@app/core/account';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-project-form',
  templateUrl: './project-form.component.html',
  styleUrls: ['./project-form.component.scss']
})
export class ProjectFormComponent implements OnInit {

  project$: Observable<Project>;
  isNew = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private projectService: ProjectService
  ) { }

  ngOnInit() {
    this.project$ = this.route.paramMap.pipe(
      switchMap(params => this.initializeProject(params.get('id')))
    );
  }

  submit(project: Project) {
    const result = this.isNew ? this.projectService.create(project) : this.projectService.update(project);
    result.subscribe(() => this.back());
  }

  back() {
    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(['/project']);
  }

  private initializeProject(idText: string): Observable<Project> {
    this.isNew = idText === 'add';
    return this.isNew ? of(new Project()) : this.projectService.getById(Number(idText));
  }

}
