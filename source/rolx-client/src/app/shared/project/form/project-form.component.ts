import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { map, } from 'rxjs/operators';

import { Customer, CustomerService, Project, ProjectService } from '@app/core/account';

@Component({
  selector: 'rolx-project-form',
  templateUrl: './project-form.component.html',
  styleUrls: ['./project-form.component.scss']
})
export class ProjectFormComponent implements OnInit {

  @Input() project: Project;
  @Input() isNew: boolean;

  customers: Customer[] = [];
  customersFiltered$: Observable<Customer[]>;

  private filterText$ = new Subject<string>();

  constructor(
    private router: Router,
    private projectService: ProjectService,
    private customerService: CustomerService,
  ) { }

  ngOnInit() {
    this.customerService.getAll()
      .subscribe(cs => this.customers = cs.sort(
        (a, b) => a.number.localeCompare(b.number) || a.name.localeCompare(b.name)));

    this.customersFiltered$ = this.filterText$.pipe(
      map(t => this.filterCustomers(t))
    );
  }

  updateFilterText(value: string) {
    this.filterText$.next(value);
  }

  validateCustomer(project: Project) {
    if (typeof project.customer === 'string' || project.customer instanceof String) {
      project.customer = null;
    }
  }

  customerDisplay(customer: Customer) {
    return customer ? customer.number + ' - ' + customer.name : undefined;
  }

  submit(project: Project) {
    const result = this.isNew ? this.projectService.create(project) : this.projectService.update(project);
    result.subscribe(() => this.back());
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

}
