import { Component, Input } from '@angular/core';

@Component({
  selector: 'rolx-days-indicator',
  templateUrl: './days-indicator.component.html',
  styleUrls: ['./days-indicator.component.scss'],
})
export class DaysIndicatorComponent {
  @Input()
  days = 0;

  @Input()
  forcePlusSign = false;

  get prefix() {
    return this.forcePlusSign && this.days > 0 ? '+' : '';
  }
}
