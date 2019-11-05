import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Customer, CustomerService, Project, ProjectService } from '@app/core/account';
import { ErrorResponse, ErrorService } from '@app/core/error';
import { SetupService } from '@app/core/setup';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'rolx-project-form',
  templateUrl: './project-form.component.html',
  styleUrls: ['./project-form.component.scss'],
})
export class ProjectFormComponent implements OnInit {

  @Input() project: Project;
  @Input() isNew: boolean;

  customers: Customer[] = [];
  customersFiltered$: Observable<Customer[]>;

  projectForm = this.fb.group({
    number: ['', [
      Validators.required,
      Validators.pattern(this.setupService.info.projectNumberPattern),
    ]],
    name: ['', Validators.required],
    customer: ['', this.validateCustomerIsSelected()],
    openUntil: [''],
  });

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private projectService: ProjectService,
    private customerService: CustomerService,
    private setupService: SetupService,
    private errorService: ErrorService,
  ) { }

  ngOnInit() {
    this.customerService.getAll()
      .subscribe(cs => {
        this.customers = cs.sort((a, b) => a.number.localeCompare(b.number) || a.name.localeCompare(b.name));

        // manually trigger the valueChanges observable to ensure the customersFiltered$ are up to date
        this.projectForm.controls.customer.setValue(this.project.customer || '');
      });

    this.customersFiltered$ = this.projectForm.controls.customer.valueChanges.pipe(
      map(value => typeof value === 'string' ? value : this.customerDisplay(value)),
      map(t => this.filterCustomers(t)),
    );

    this.projectForm.patchValue(this.project);
  }

  hasError(controlName: string, errorName: string) {
    return this.projectForm.controls[controlName].hasError(errorName);
  }

  customerDisplay(customer: Customer) {
    return customer ? customer.number + ' - ' + customer.name : undefined;
  }

  submit() {
    Object.assign(this.project, this.projectForm.value);

    const request = this.isNew ? this.projectService.create(this.project) : this.projectService.update(this.project);
    request.subscribe(() => this.back(), err => this.handleError(err));
  }

  back() {
    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(['/project']);
  }

  private filterCustomers(filterText: string): Customer[] {
    filterText = filterText.toLocaleLowerCase();
    return this.customers
      .filter(customer => this.customerDisplay(customer).toLocaleLowerCase().includes(filterText))
      .slice(0, 8);
  }

  private validateCustomerIsSelected(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      if (typeof control.value !== 'string') {
        return null; // ok
      }

      const customer = this.customers.find((c => this.customerDisplay(c) === control.value));
      if (customer) {
        control.setValue(customer);
        return null; // ok
      }

      return { noCustomer: true}; // not ok
    };
  }

  private handleError(errorResponse: ErrorResponse) {
    if (!errorResponse.tryToHandleWith(this.projectForm)) {
      this.errorService.notifyGeneralError();
    }
  }

}
