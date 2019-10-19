import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { Customer, CustomerService } from '@app/core/account';

@Component({
  selector: 'rolx-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.scss']
})
export class CustomerFormComponent implements OnInit {

  customer$: Observable<Customer>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private customerService: CustomerService
  ) { }

  ngOnInit() {
    this.customer$ = this.route.paramMap.pipe(
      switchMap(params => this.customerService.getById(Number(params.get('id'))))
    );
  }

  submit(customer: Customer) {
    this.customerService.update(customer).subscribe(() => this.back());
  }

  back() {
    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(['/customer']);
  }
}
