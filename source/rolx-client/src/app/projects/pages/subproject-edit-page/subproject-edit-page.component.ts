import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subproject } from '@app/projects/core/subproject';
import { SubprojectService } from '@app/projects/core/subproject.service';
import { Observable, of, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-subproject-edit-page',
  templateUrl: './subproject-edit-page.component.html',
  styleUrls: ['./subproject-edit-page.component.scss'],
})
export class SubprojectEditPageComponent {
  readonly subproject$ = this.route.paramMap.pipe(
    switchMap((params) => this.initializeSubproject(params.get('id'))),
    catchError((e) => {
      if (e.status === 404) {
        // noinspection JSIgnoredPromiseFromCall
        this.router.navigate(['/four-oh-four']);
      }

      return throwError(() => e);
    }),
  );

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private subprojectService: SubprojectService,
  ) {}

  private initializeSubproject(idText: string | null): Observable<Subproject> {
    return idText === 'new' ? of(new Subproject()) : this.subprojectService.getById(Number(idText));
  }
}
