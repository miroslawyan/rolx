import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AccountModule } from '@app/account';
import { AppImportModule } from '@app/app-import.module';
import { WeekTableCellComponent } from '@app/work-record/shared/week-table/cell/week-table-cell.component';
import {
  MonthPageComponent,
  WeekPageComponent,
} from './pages';
import {
  EntriesEditComponent,
  MonthTableComponent,
  MultiEntriesDialogComponent,
  WeekTableComponent,
} from './shared';

@NgModule({
  imports: [
    AccountModule,
    AppImportModule,
    CommonModule,
  ],
  declarations: [
    EntriesEditComponent,
    MonthPageComponent,
    MonthTableComponent,
    MultiEntriesDialogComponent,
    WeekPageComponent,
    WeekTableCellComponent,
    WeekTableComponent,
  ],
  entryComponents: [
    MultiEntriesDialogComponent,
  ],
  exports: [
    MonthPageComponent,
    WeekPageComponent,
  ],
})
export class WorkRecordModule { }
