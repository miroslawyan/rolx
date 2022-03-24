import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '@app/auth/core/auth.service';
import { mapPlainToInstance } from '@app/core/util/operators';
import { UserMonthReport } from '@app/reports/core/user-month-report';
import { environment } from '@env/environment';
import * as moment from 'moment';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class UserReportService {
  private static readonly Url = environment.apiBaseUrl + '/v1/user-report';

  constructor(private httpClient: HttpClient, private authService: AuthService) {}

  getMonthReport(userId: string, month: moment.Moment): Observable<UserMonthReport> {
    const url = UserReportService.UrlWithId(userId) + '/month/' + month.format('YYYY-MM');
    return this.httpClient.get(url).pipe(
      mapPlainToInstance(UserMonthReport),
      tap((report) => report.validateModel()),
    );
  }

  private static UrlWithId(id: string) {
    return UserReportService.Url + '/' + id;
  }
}
