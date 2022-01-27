import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { YearInfo } from '@app/records/core/year-info';
import { YearInfoService } from '@app/records/core/year-info.service';
import moment from 'moment';
import { Observable } from 'rxjs';
import { map, switchMap} from 'rxjs/operators';

@Component({
  selector: 'rolx-year-overview-page',
  templateUrl: './year-overview-page.component.html',
})
export class YearOverviewPageComponent {
  year$: Observable<number>;
  yearInfo$: Observable<YearInfo>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    yearInfoService: YearInfoService,
  ) {
    this.year$ = this.route.paramMap.pipe(
      map(params => moment(params.get('date') ?? {})),
      map(date => date.clone().year()),
    );
    this.yearInfo$ = this.year$.pipe(
      switchMap( year => yearInfoService.getYearInfo(year)),
    );
  }

  async previous(year: number): Promise<void> {
    await this.navigateTo(year - 1);
  }

  async next(year: number): Promise<void> {
    await this.navigateTo(year + 1 );
  }

  private async navigateTo(date: number): Promise<void> {
    await this.router.navigate(['year-overview', date]);
  }
}
