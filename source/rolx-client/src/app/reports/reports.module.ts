import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { CoreModule } from '@app/core/core.module';

import { ExportPageComponent } from './pages/export-page/export-page.component';
import { UserMonthReportPageComponent } from './pages/user-month-report-page/user-month-report-page.component';
import { DaysIndicatorComponent } from './shared/days-indicator/days-indicator.component';
import { ExportMonthCardComponent } from './shared/export-month-card/export-month-card.component';
import { ExportRangeCardComponent } from './shared/export-range-card/export-range-card.component';
import { ExportSubprojectCardComponent } from './shared/export-subproject-card/export-subproject-card.component';

const exportedComponents = [
  DaysIndicatorComponent,
  ExportMonthCardComponent,
  ExportPageComponent,
  ExportRangeCardComponent,
  ExportSubprojectCardComponent,
  UserMonthReportPageComponent,
];

@NgModule({
  imports: [AppImportModule, CommonModule, CoreModule],
  declarations: exportedComponents,
  exports: exportedComponents,
})
export class ReportsModule {}
