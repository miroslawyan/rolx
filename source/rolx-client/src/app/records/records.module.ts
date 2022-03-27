import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { CoreModule } from '@app/core/core.module';
import { ProjectsModule } from '@app/projects/projects.module';
import { WeekPageComponent } from '@app/records/pages/week-page/week-page.component';
import { YearOverviewPageComponent } from '@app/records/pages/year-overview-page/year-overview-page.component';
import { BalanceIndicatorComponent } from '@app/records/shared/balance-indicator/balance-indicator.component';
import { DurationEditComponent } from '@app/records/shared/duration-edit/duration-edit.component';
import { HolidayTableComponent } from '@app/records/shared/holiday-table/holiday-table.component';
import { MonthlyWorkTimeTableComponent } from '@app/records/shared/monthly-work-time-table/monthly-work-time-table.component';
import { MultiEntriesDialogComponent } from '@app/records/shared/multi-entries-dialog/multi-entries-dialog.component';
import { PaidLeaveSelectComponent } from '@app/records/shared/paid-leave-select/paid-leave-select.component';
import { ReasonDialogComponent } from '@app/records/shared/paid-leave-select/reason-dialog/reason-dialog.component';
import { WeekTableCellComponent } from '@app/records/shared/week-table/cell/week-table-cell.component';
import { WeekTableComponent } from '@app/records/shared/week-table/week-table.component';

import { SetEditLockFormComponent } from './shared/set-edit-lock-form/set-edit-lock-form.component';

const exportedDeclarations = [
  BalanceIndicatorComponent,
  SetEditLockFormComponent,
  WeekPageComponent,
  YearOverviewPageComponent,
];

@NgModule({
  imports: [AppImportModule, CommonModule, CoreModule, ProjectsModule],
  declarations: [
    ...exportedDeclarations,
    DurationEditComponent,
    HolidayTableComponent,
    MonthlyWorkTimeTableComponent,
    MultiEntriesDialogComponent,
    PaidLeaveSelectComponent,
    ReasonDialogComponent,
    WeekTableCellComponent,
    WeekTableComponent,
  ],
  exports: exportedDeclarations,
})
export class RecordsModule {}
