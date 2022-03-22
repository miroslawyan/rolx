import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { CoreModule } from '@app/core/core.module';
import { ProjectsModule } from '@app/projects/projects.module';
import { RecordsModule } from '@app/records/records.module';

import { ExportPageComponent } from './pages/export-page/export-page.component';
import { UserMonthReportPageComponent } from './pages/user-month-report-page/user-month-report-page.component';
import { ExportMonthCardComponent } from './shared/export-month-card/export-month-card.component';
import { ExportRangeCardComponent } from './shared/export-range-card/export-range-card.component';

const exportedComponents = [
  ExportMonthCardComponent,
  ExportPageComponent,
  ExportRangeCardComponent,
  UserMonthReportPageComponent,
];

@NgModule({
  imports: [AppImportModule, CommonModule, CoreModule, ProjectsModule, RecordsModule],
  declarations: exportedComponents,
  exports: exportedComponents,
})
export class ReportsModule {}
