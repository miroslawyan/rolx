import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { ProjectsModule } from '@app/projects';
import { WeekTableCellComponent } from '@app/records/shared/week-table/cell/week-table-cell.component';
import {
  MonthPageComponent,
  WeekPageComponent,
} from './pages';
import {
  DurationEditComponent,
  MonthTableComponent,
  MultiEntriesDialogComponent,
  WeekTableComponent,
} from './shared';

@NgModule({
  imports: [
    AppImportModule,
    CommonModule,
    ProjectsModule,
  ],
  declarations: [
    DurationEditComponent,
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
export class RecordsModule { }
