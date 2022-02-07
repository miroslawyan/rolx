import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'rolx-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
})
export class ToolbarComponent {
  @Input()
  isHandset = false;

  @Output()
  menuClicked = new EventEmitter();
}
