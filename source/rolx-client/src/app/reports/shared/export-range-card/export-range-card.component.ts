import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FileSaverService } from '@app/core/util/file-saver.service';
import { ExportService } from '@app/reports/core/export.service';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'rolx-export-range-card',
  templateUrl: './export-range-card.component.html',
  styleUrls: ['./export-range-card.component.scss'],
})
export class ExportRangeCardComponent {
  readonly beginControl = new FormControl(null, Validators.required);
  readonly endControl = new FormControl(null, Validators.required);
  readonly form = new FormGroup({ begin: this.beginControl, end: this.endControl });

  constructor(private exportService: ExportService, private fileSaverService: FileSaverService) {}

  async exportRange(): Promise<void> {
    const data = await lastValueFrom(
      this.exportService.download(
        this.beginControl.value,
        this.endControl.value.clone().add(1, 'days'),
      ),
    );
    this.fileSaverService.save(data);
  }
}
