import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from '@app/projects/core/project';
import { ProjectService } from '@app/projects/core/project.service';
import { Observable, of, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-project-edit-page',
  templateUrl: './project-edit-page.component.html',
  styleUrls: ['./project-edit-page.component.scss'],
})
export class ProjectEditPageComponent implements OnInit {

  project$: Observable<Project>;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private projectService: ProjectService,
  ) { }

  ngOnInit() {
    this.project$ = this.route.paramMap.pipe(
      switchMap(params => this.initializeProject(params.get('id'))),
      catchError(e => {
        if (e.status === 404) {
          // noinspection JSIgnoredPromiseFromCall
          this.router.navigate(['/four-oh-four']);
        }

        return throwError(e);
      }),
    );
  }

  private initializeProject(idText: string | null): Observable<Project> {
    return idText === 'new' ? of(new Project()) : this.projectService.getById(Number(idText));
  }

}
