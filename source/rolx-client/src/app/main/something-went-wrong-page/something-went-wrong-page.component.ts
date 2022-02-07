import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'rolx-something-went-wrong-page',
  templateUrl: './something-went-wrong-page.component.html',
  styleUrls: ['./something-went-wrong-page.component.scss'],
})
export class SomethingWentWrongPageComponent {
  constructor(private router: Router, private location: Location) {}

  navigateHome() {
    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(['/']);
  }

  retry() {
    this.location.back();
  }
}
