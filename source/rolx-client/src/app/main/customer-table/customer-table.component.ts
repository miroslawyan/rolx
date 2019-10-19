import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';

import { Customer, CustomerService } from '@app/core/account';

@Component({
  selector: 'rolx-customer-table',
  templateUrl: './customer-table.component.html',
  styleUrls: ['./customer-table.component.scss']
})
export class CustomerTableComponent implements OnInit {

  displayedColumns: string[] = ['number', 'name', 'tools'];
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
