import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@app/auth/core/auth.service';
import { UserMonthReport } from '@app/reports/core/user-month-report';
import { UserReportService } from '@app/reports/core/user-report.service';
import { UserMonthReportParams } from '@app/reports/pages/user-month-report-page/user-month-report-params';
import { NEVER, Observable, switchMap, withLatestFrom } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Component({
  selector: 'rolx-user-month-report-page',
  templateUrl: './user-month-report-page.component.html',
  styleUrls: ['./user-month-report-page.component.scss'],
})
export class UserMonthReportPageComponent {
  readonly routeParams$: Observable<UserMonthReportParams> = this.route.url.pipe(
    withLatestFrom(this.route.paramMap, this.route.queryParamMap),
    map(([, paramMap, queryParamMap]) =>
      UserMonthReportParams.evaluate(
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

  readonly report$: Observable<UserMonthReport> = this.routeParams$.pipe(
    switchMap((params) => this.userReportService.getMonthReport(params.userId, params.month)),
  );

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userReportService: UserReportService,
    private authService: AuthService,
  ) {}
}
