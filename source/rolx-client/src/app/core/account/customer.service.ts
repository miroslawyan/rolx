import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, mapTo } from 'rxjs/operators';

import { ErrorResponse } from '@app/core/error';
import { environment } from '@env/environment';
import { Customer } from './customer';

const CustomerUrl = environment.apiBaseUrl + '/v1/customer';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private httpClient: HttpClient) { }

  private static UrlWithId(id: number) {
    return CustomerUrl + '/' + id;
  }

  getAll(): Observable<Customer[]> {
    return this.httpClient.get<Customer[]>(CustomerUrl);
  }

  getById(id: number): Observable<Customer> {
    return this.httpClient.get<Customer>(CustomerService.UrlWithId(id));
  }

  create(customer: Customer): Observable<Customer> {
    customer.id = undefined;
    return this.httpClient.post<Customer>(CustomerUrl, customer).pipe(
      catchError(e => throwError(new ErrorResponse(e))),
    );
  }

  update(customer: Customer): Observable<Customer> {
    return this.httpClient.put(CustomerService.UrlWithId(customer.id), customer).pipe(
      mapTo(customer),
      catchError(e => throwError(new ErrorResponse(e))),
    );
  }
}
