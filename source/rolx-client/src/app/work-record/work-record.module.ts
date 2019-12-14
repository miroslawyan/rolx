import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AccountModule } from '@app/account';
import { AppImportModule } from '@app/app-import.module';
import {
  MonthPageComponent,
  WeekPageComponent,
} from './pages';
import {
  EntriesEditComponent,
  MonthTableComponent,
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
    WeekPageComponent,
    WeekTableComponent,
  ],
  exports: [
    MonthPageComponent,
    WeekPageComponent,
  ],
})
export class WorkRecordModule { }
