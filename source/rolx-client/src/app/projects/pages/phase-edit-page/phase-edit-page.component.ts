import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Phase } from '@app/projects/core/phase';
import { Project } from '@app/projects/core/project';
import { ProjectService } from '@app/projects/core/project.service';
import { Observable, throwError } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-phase-edit-page',
  templateUrl: './phase-edit-page.component.html',
  styleUrls: ['./phase-edit-page.component.scss'],
})
export class PhaseEditPageComponent {
  readonly project$: Observable<Project> = this.route.paramMap.pipe(
    switchMap((params) => this.initializeProject(params.get('id'), params.get('phaseId'))),
    catchError((e) => {
      if (e.status === 404) {
        // noinspection JSIgnoredPromiseFromCall
        this.router.navigate(['/four-oh-four']);
      }

      return throwError(e);
    }),
  );

  phase = new Phase();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private projectService: ProjectService,
  ) {}

  private initializeProject(
    projectIdText: string | null,
    phaseIdText: string | null,
  ): Observable<Project> {
    return this.projectService
      .getById(Number(projectIdText))
      .pipe(map((project) => this.initializePhase(project, phaseIdText)));
  }

  private initializePhase(project: Project, phaseIdText: string | null): Project {
    const phaseId = Number(phaseIdText);

    const phase =
      phaseIdText === 'new' ? project.addPhase() : project.phases.find((ph) => ph.id === phaseId);

    if (phase != null) {
      this.phase = phase;
    } else {
      // noinspection JSIgnoredPromiseFromCall
      this.router.navigate(['/four-oh-four']);
    }

    return project;
  }
}
