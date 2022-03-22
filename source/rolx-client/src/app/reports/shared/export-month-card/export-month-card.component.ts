import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { FileSaverService } from '@app/core/util/file-saver.service';
import { ExportService } from '@app/reports/core/export.service';
import * as moment from 'moment';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'rolx-export-month-card',
  templateUrl: './export-month-card.component.html',
  styleUrls: ['./export-month-card.component.scss'],
})
export class ExportMonthCardComponent {
  readonly monthControl = new FormControl(moment(), Validators.required);

  constructor(private exportService: ExportService, private fileSaverService: FileSaverService) {}

  chosenYearHandler(normalizedYear: moment.Moment) {
    console.log(normalizedYear);
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
    const data = await lastValueFrom(this.exportService.download(this.monthControl.value));
    this.fileSaverService.save(data);
  }
}
