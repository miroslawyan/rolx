import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'rolx-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
})
export class ToolbarComponent implements OnInit {

  @Input()
  isHandset = false;

  @Output()
  menuClicked = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

}
