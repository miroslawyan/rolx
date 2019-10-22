import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of, Subject } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

import { Customer, CustomerService, Project, ProjectService } from '@app/core/account';

@Component({
  selector: 'rolx-project-form',
  templateUrl: './project-form.component.html',
  styleUrls: ['./project-form.component.scss']
})
export class ProjectFormComponent implements OnInit {

  project$: Observable<Project>;
  isNew = false;
  customers: Customer[] = [];
  customersFiltered$: Observable<Customer[]>;

  private filterText$ = new Subject<string>();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private projectService: ProjectService,
    private customerService: CustomerService,
  ) { }

  ngOnInit() {
    this.project$ = this.route.paramMap.pipe(
      switchMap(params => this.initializeProject(params.get('id')))
    );

    this.customerService.getAll()
      .subscribe(cs => this.customers = cs.sort((a, b) => a.name.localeCompare(b.name)));

    this.customersFiltered$ = this.filterText$.pipe(
      map(t => this.filterCustomers(t))
    );
  }

  updateFilterText(value: string) {
    this.filterText$.next(value);
  }

  validateCustomer(project: Project) {
    if (project.raw.customerId == null) {
      project.customer = undefined;
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

  private initializeProject(idText: string): Observable<Project> {
    this.isNew = idText === 'add';
    return this.isNew ? of(new Project()) : this.projectService.getById(Number(idText));
  }

  private filterCustomers(filterText: string): Customer[] {
    filterText = filterText.toLocaleLowerCase();
    return this.customers
      .filter(customer => this.customerDisplay(customer).toLocaleLowerCase().includes(filterText))
      .slice(0, 8);
  }

}
