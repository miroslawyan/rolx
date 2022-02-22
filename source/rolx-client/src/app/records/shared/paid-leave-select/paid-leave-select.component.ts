import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { assertDefined } from '@app/core/util/utils';
import { PaidLeaveType } from '@app/records/core/paid-leave-type';
import { Record } from '@app/records/core/record';
import { Subject, Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';

import { ReasonDialogComponent, ReasonDialogData } from './reason-dialog/reason-dialog.component';

@Component({
  selector: 'rolx-paid-leave-select',
  templateUrl: './paid-leave-select.component.html',
})
export class PaidLeaveSelectComponent implements OnInit, OnDestroy {
  private readonly subscriptions = new Subscription();
  private recordShadow!: Record;

  private typeShadow?: PaidLeaveType;
  private typeSubject = new Subject<PaidLeaveType | undefined>();

  readonly PaidLeaveType = PaidLeaveType;

  @Input()
  get record() {
    return this.recordShadow;
  }
  set record(value) {
    this.recordShadow = value;
    this.typeShadow = value.paidLeaveType;
  }

  @Output()
  changed = new EventEmitter<Record>();

  get type() {
    return this.typeShadow;
  }
  set type(value) {
    this.typeShadow = value;
    this.typeSubject.next(value);
  }

  constructor(private dialog: MatDialog) {
    this.subscriptions.add(
      this.typeSubject
        .pipe(filter((t) => t !== PaidLeaveType.Other))
        .subscribe((t) => this.submit(t, undefined)),
    );

    this.subscriptions.add(
      this.typeSubject
        .pipe(filter((t) => t === PaidLeaveType.Other))
        .subscribe(() => this.requestReason()),
    );
  }

  ngOnInit(): void {
    assertDefined(this, 'recordShadow');
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  iconForPaidLeaveType(type: PaidLeaveType): string {
    switch (type) {
      case PaidLeaveType.Vacation:
        return 'kitesurfing';
      case PaidLeaveType.Sickness:
        return 'healing';
      case PaidLeaveType.MilitaryService:
        return 'military_tech';
      case PaidLeaveType.Other:
        return 'question_mark';
    }
  }

  textForPaidLeaveType(type: PaidLeaveType): string {
    switch (type) {
      case PaidLeaveType.Vacation:
        return 'Ferien';
      case PaidLeaveType.Sickness:
        return 'Krank';
      case PaidLeaveType.MilitaryService:
        return 'Militär';
      case PaidLeaveType.Other:
        return 'Sonstige';
    }
  }

  private requestReason() {
    const data: ReasonDialogData = {
      reason: this.record.paidLeaveReason,
    };

    this.dialog
      .open(ReasonDialogComponent, {
        closeOnNavigation: true,
        data,
      })
      .afterClosed()
      .subscribe((reason) => {
        if (reason != null && reason !== '') {
          this.submit(PaidLeaveType.Other, reason);
        } else {
          this.typeShadow = undefined;
        }
      });
  }

  private submit(type: PaidLeaveType | undefined, reason: string | undefined) {
    const record = this.record.updatePaidLeaveType(type, reason);
    this.changed.emit(record);
  }
}
