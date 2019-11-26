import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Phase, Project, ProjectService } from '@app/account/core';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-phase-list-page',
  templateUrl: './project-detail-page.component.html',
  styleUrls: ['./project-detail-page.component.scss'],
})
export class ProjectDetailPageComponent implements OnInit {

  project$: Observable<Project>;

  editedPhase: Phase;

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

  private initializeProject(idText: string): Observable<Project> {
    return this.projectService.getById(Number(idText));
  }
}
