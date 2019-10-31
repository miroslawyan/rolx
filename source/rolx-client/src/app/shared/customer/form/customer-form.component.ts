import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { Customer, CustomerService } from '@app/core/account';
import { ErrorResponse, ErrorService } from '@app/core/error';
import { SetupService } from '@app/core/setup';

@Component({
  selector: 'rolx-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.scss']
})
export class CustomerFormComponent implements OnInit {

  @Input() customer: Customer;
  @Input() isNew: boolean;

  customerForm = this.fb.group({
    number: ['', [
      Validators.required,
      Validators.pattern(this.setupService.info.customerNumberPattern)
    ]],
    name: ['', Validators.required],
  });

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private customerService: CustomerService,
    private setupService: SetupService,
    private errorService: ErrorService
  ) { }

  ngOnInit() {
    this.customerForm.patchValue(this.customer);
  }

  hasError(controlName: string, errorName: string) {
    return this.customerForm.controls[controlName].hasError(errorName);
  }

  submit() {
    Object.assign(this.customer, this.customerForm.value);

    const request = this.isNew ? this.customerService.create(this.customer) : this.customerService.update(this.customer);
    request.subscribe(() => this.back(), err => this.handleError(err));
  }

  back() {
    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(['/customer']);
  }

  private handleError(errorResponse: ErrorResponse) {
    if (!errorResponse.tryToHandleWith(this.customerForm)) {
      this.errorService.notifyGeneralError();
    }
  }
}
