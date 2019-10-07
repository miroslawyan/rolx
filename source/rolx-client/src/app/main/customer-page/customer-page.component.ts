import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';

import { Customer, CustomerService } from '@app/core/account';

@Component({
  selector: 'rolx-customer-page',
  templateUrl: './customer-page.component.html',
  styleUrls: ['./customer-page.component.scss']
})
export class CustomerPageComponent implements OnInit {

  displayedColumns: string[] = ['number', 'name'];
  customers: MatTableDataSource<Customer>;

  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
    this.customerService.getAll()
      .subscribe(cs => {
        this.customers = new MatTableDataSource<Customer>(cs);
        this.customers.sort = this.sort;
      });
  }

}
