import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@app/auth/core/auth.service';
import { ActivityService } from '@app/projects/core/activity.service';
import { EditLockService } from '@app/records/core/edit-lock.service';
import { WorkRecordService } from '@app/records/core/work-record.service';
import { WeekPageParams } from '@app/records/pages/week-page/week-page-params';
import { UserService } from '@app/users/core/user.service';
import { forkJoin, NEVER, Observable, of, withLatestFrom } from 'rxjs';
import { catchError, map, switchMap, tap } from 'rxjs/operators';

@Component({
  selector: 'rolx-week-page',
  templateUrl: './week-page.component.html',
})
export class WeekPageComponent {
  readonly routeParams$: Observable<WeekPageParams> = this.route.url.pipe(
    withLatestFrom(this.route.paramMap, this.route.queryParamMap),
    map(([, paramMap, queryParamMap]) =>
      WeekPageParams.evaluate(
        paramMap,
        queryParamMap,
        this.authService.currentApprovalOrError.user.id,
      ),
    ),
    catchError(() => {
      // noinspection JSIgnoredPromiseFromCall
      this.router.navigate(['four-oh-four']);
      return NEVER;
    }),
  );

  readonly recordsActivitiesUser$ = this.routeParams$.pipe(
    tap(() => this.editLockService.refresh()),
    switchMap((params) =>
      forkJoin([
        this.workRecordService.getRange(params.userId, params.monday, params.nextMonday),
        this.activityService.getSuitable(params.userId, params.monday),
        params.isCurrentUser
          ? of(this.authService.currentApprovalOrError.user)
          : this.userService.getById(params.userId),
      ]),
    ),
  );

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private workRecordService: WorkRecordService,
    private activityService: ActivityService,
    private userService: UserService,
    private authService: AuthService,
    private editLockService: EditLockService,
  ) {}
}
