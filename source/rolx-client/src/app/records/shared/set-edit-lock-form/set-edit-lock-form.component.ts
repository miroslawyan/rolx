import { Component, OnDestroy } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { EditLockService } from '@app/records/core/edit-lock.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'rolx-set-edit-lock-form',
  templateUrl: './set-edit-lock-form.component.html',
  styleUrls: ['./set-edit-lock-form.component.scss'],
})
export class SetEditLockFormComponent implements OnDestroy {
  private readonly subscription: Subscription;

  readonly dateControl = new FormControl(null, Validators.required);

  get isModified(): boolean {
    return !this.editLockService.date.isSame(this.dateControl.value, 'day');
  }

  constructor(private readonly editLockService: EditLockService) {
    this.subscription = this.editLockService.date$.subscribe((date) =>
      this.dateControl.reset(date),
    );
    this.editLockService.refresh();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async commit(): Promise<void> {
    await this.editLockService.set(this.dateControl.value);
  }

  cancel(): void {
    this.dateControl.reset(this.editLockService.date);
  }
}
