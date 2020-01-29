import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';
import { Role } from '@app/auth/core';
import { UserData } from '@app/auth/core/user.data';
import { UserService } from '@app/user-management/core/user.service';

@Component({
  selector: 'rolx-user-table',
  templateUrl: './user-table.component.html',
  styleUrls: ['./user-table.component.scss'],
})
export class UserTableComponent implements OnInit {

  displayedColumns: string[] = ['avatar', 'fullName', 'email', 'role'];
  users: MatTableDataSource<UserData>;
  Role = Role;

  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.getAll()
      .subscribe(users => {
        this.users = new MatTableDataSource<UserData>(users);
        this.users.sort = this.sort;
      });
  }

}
