import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { Customer, CustomerService } from '@app/core/account';
import { ErrorResponse, ErrorService } from '@app/core/error';
import { SetupService } from '@app/core/setup';

@Component({
  selector: 'rolx-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.scss']
})
export class CustomerFormComponent implements OnInit {

  isNew = false;

  customerForm = this.fb.group({
    id: [''],
    number: ['', [
      Validators.required,
      Validators.pattern(this.setupService.info.customerNumberPattern)
    ]],
    name: ['', Validators.required],
  });

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private customerService: CustomerService,
    private setupService: SetupService,
    private errorService: ErrorService
  ) { }

  ngOnInit() {
    this.route.paramMap.pipe(
      switchMap(params => this.initializeCustomer(params.get('id')))
    ).subscribe(c => this.customerForm.patchValue(c));
  }

  hasError(controlName: string, errorName: string) {
    return this.customerForm.controls[controlName].hasError(errorName);
  }

  submit() {
    const customer = this.customerForm.value;
    const result = this.isNew ? this.customerService.create(customer) : this.customerService.update(customer);
    result.subscribe(() => this.back(), err => this.handleError(err));
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

  private handleError(errorResponse: ErrorResponse) {
    if (!errorResponse.tryToHandleWith(this.customerForm)) {
      this.errorService.notifyGeneralError();
    }
  }
}
