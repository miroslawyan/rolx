import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { CoreModule } from '@app/core';
import { ProjectsModule } from '@app/projects';
import {
  WeekPageComponent,
} from './pages';
import {
  BalanceIndicatorComponent,
  DurationEditComponent,
  MultiEntriesDialogComponent,
  PaidLeaveSelectComponent,
  WeekTableComponent,
} from './shared';
import { ReasonDialogComponent } from './shared/paid-leave-select/reason-dialog/reason-dialog.component';
import { WeekTableCellComponent } from './shared/week-table/cell/week-table-cell.component';

@NgModule({
  imports: [
    AppImportModule,
    CommonModule,
    ProjectsModule,
    CoreModule,
  ],
  declarations: [
    BalanceIndicatorComponent,
    DurationEditComponent,
    MultiEntriesDialogComponent,
    PaidLeaveSelectComponent,
    ReasonDialogComponent,
    WeekPageComponent,
    WeekTableCellComponent,
    WeekTableComponent,
  ],
  entryComponents: [
    MultiEntriesDialogComponent,
  ],
  exports: [
    WeekPageComponent,
    BalanceIndicatorComponent,
  ],
})
export class RecordsModule { }
