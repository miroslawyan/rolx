import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@app/auth/core/auth.service';
import { Role } from '@app/auth/core/role';
import { Subproject } from '@app/projects/core/subproject';
import { SubprojectService } from '@app/projects/core/subproject.service';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-subproject-detail-page',
  templateUrl: './subproject-detail-page.component.html',
  styleUrls: ['./subproject-detail-page.component.scss'],
})
export class SubprojectDetailPageComponent {
  readonly mayEdit = this.authService.currentApprovalOrError.user.role >= Role.Supervisor;
  readonly mayExport = this.authService.currentApprovalOrError.user.role >= Role.Supervisor;

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
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly subprojectService: SubprojectService,
    private readonly authService: AuthService,
  ) {}

  private initializeSubproject(idText: string | null): Observable<Subproject> {
    return this.subprojectService.getById(Number(idText));
  }
}
