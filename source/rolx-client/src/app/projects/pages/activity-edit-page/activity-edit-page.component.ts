import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Activity } from '@app/projects/core/activity';
import { Subproject } from '@app/projects/core/subproject';
import { SubprojectService } from '@app/projects/core/subproject.service';
import { Observable, throwError } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-activity-edit-page',
  templateUrl: './activity-edit-page.component.html',
  styleUrls: ['./activity-edit-page.component.scss'],
})
export class ActivityEditPageComponent {
  readonly subproject$: Observable<Subproject> = this.route.paramMap.pipe(
    switchMap((params) => this.initializeSubproject(params.get('id'), params.get('activityId'))),
    catchError((e) => {
      if (e.status === 404) {
        // noinspection JSIgnoredPromiseFromCall
        this.router.navigate(['/four-oh-four']);
      }

      return throwError(() => e);
    }),
  );

  activity = new Activity();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private subprojectService: SubprojectService,
  ) {}

  private initializeSubproject(
    subprojectIdText: string | null,
    activityIdText: string | null,
  ): Observable<Subproject> {
    return this.subprojectService
      .getById(Number(subprojectIdText))
      .pipe(map((subproject) => this.initializeActivity(subproject, activityIdText)));
  }

  private initializeActivity(subproject: Subproject, activityIdText: string | null): Subproject {
    const activityId = Number(activityIdText);

    const activity =
      activityIdText === 'new'
        ? subproject.addActivity()
        : subproject.activities.find((ph) => ph.id === activityId);

    if (activity != null) {
      this.activity = activity;
    } else {
      // noinspection JSIgnoredPromiseFromCall
      this.router.navigate(['/four-oh-four']);
    }

    return subproject;
  }
}
