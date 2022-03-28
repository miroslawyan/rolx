import { Component, Input, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import { FileSaverService } from '@app/core/util/file-saver.service';
import { assertDefined } from '@app/core/util/utils';
import { Subproject } from '@app/projects/core/subproject';
import { ExportService } from '@app/reports/core/export.service';
import * as moment from 'moment';
import { lastValueFrom } from 'rxjs';

export const MONTH_FORMAT = {
  parse: {
    dateInput: 'MM.YYYY',
  },
  display: {
    dateInput: 'MM.YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'rolx-export-subproject-card',
  templateUrl: './export-subproject-card.component.html',
  styleUrls: ['./export-subproject-card.component.scss'],
  providers: [{ provide: MAT_DATE_FORMATS, useValue: MONTH_FORMAT }],
})
export class ExportSubprojectCardComponent implements OnInit {
  readonly monthControl = new FormControl(moment(), [
    Validators.required,
    (control) =>
      this.subproject?.startDate?.isSameOrBefore(control.value, 'month') ?? true
        ? null
        : { monthBeforeStart: true },
  ]);

  @Input()
  subproject!: Subproject;

  @Input()
  noTitle = false;

  constructor(private exportService: ExportService, private fileSaverService: FileSaverService) {}

  ngOnInit(): void {
    assertDefined(this, 'subproject');
  }

  chosenYearHandler(normalizedYear: moment.Moment) {
    const ctrlValue = this.monthControl.value ?? moment().month(0);
    ctrlValue.year(normalizedYear.year());
    this.monthControl.setValue(ctrlValue);
  }

  chosenMonthHandler(normalizedMonth: moment.Moment, monthPicker: any) {
    const ctrlValue = this.monthControl.value;
    ctrlValue.month(normalizedMonth.month());
    this.monthControl.setValue(ctrlValue);
    monthPicker.close();
  }

  async exportMonth(): Promise<void> {
    let startDate = this.subproject.startDate;
    let endDate: moment.Moment | undefined = this.monthControl.value as moment.Moment;

    if (startDate != null) {
      endDate = endDate.clone().add(1, 'M').startOf('month');
    } else {
      startDate = endDate;
      endDate = undefined;
    }

    const data = await lastValueFrom(
      this.exportService.download(this.subproject, startDate, endDate),
    );
    this.fileSaverService.save(data);
  }
}
