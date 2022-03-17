import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppImportModule } from '@app/app-import.module';
import { CoreModule } from '@app/core/core.module';
import { ProjectsModule } from '@app/projects/projects.module';
import { RecordsModule } from '@app/records/records.module';

import { UserMonthReportPageComponent } from './pages/user-month-report-page/user-month-report-page.component';

@NgModule({
  imports: [AppImportModule, CommonModule, CoreModule, ProjectsModule, RecordsModule],
  declarations: [UserMonthReportPageComponent],
  exports: [UserMonthReportPageComponent],
})
export class ReportsModule {}
