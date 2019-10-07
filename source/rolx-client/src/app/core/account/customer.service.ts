import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {  mapTo } from 'rxjs/operators';

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
    return this.httpClient.post<Customer>(CustomerUrl, customer);
  }

  update(customer: Customer): Observable<void> {
    return this.httpClient.put(CustomerService.UrlWithId(customer.id), customer).pipe(
      mapTo(void 0)
    );
  }
}
