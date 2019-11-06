import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Customer, CustomerService } from '@app/core/account';
import { Observable, of, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-customer-edit-page',
  templateUrl: './customer-edit-page.component.html',
  styleUrls: ['./customer-edit-page.component.scss'],
})
export class CustomerEditPageComponent implements OnInit {

  customer$: Observable<Customer>;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private customerService: CustomerService,
  ) { }

  ngOnInit() {
    this.customer$ = this.route.paramMap.pipe(
      switchMap(params => this.initializeCustomer(params.get('id'))),
      catchError(e => {
        if (e.status === 404) {
          // noinspection JSIgnoredPromiseFromCall
          this.router.navigate(['/four-oh-four']);
        }

        return throwError(e);
      }),
    );
  }

  private initializeCustomer(idText: string): Observable<Customer> {
    return idText === 'add' ? of({
      id: undefined,
      number: '',
      name: '',
    }) : this.customerService.getById(Number(idText));
  }

}
