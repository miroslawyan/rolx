import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { Customer, CustomerService } from '@app/core/account';

@Component({
  selector: 'rolx-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.scss']
})
export class CustomerFormComponent implements OnInit {

  customer$: Observable<Customer>;
  isNew = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private customerService: CustomerService
  ) { }

  ngOnInit() {
    this.customer$ = this.route.paramMap.pipe(
      switchMap(params => this.initializeCustomer(params.get('id')))
    );
  }

  submit(customer: Customer) {
    const result = this.isNew ? this.customerService.create(customer) : this.customerService.update(customer);
    result.subscribe(() => this.back());
  }

  back() {
    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(['/customer']);
  }

  private initializeCustomer(idText: string): Observable<Customer> {
    this.isNew = idText === 'add';
    return this.isNew ? of({
        id: undefined,
        number: '',
        name: '',
      }) : this.customerService.getById(Number(idText));
  }
}
