import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';
import { AuthService, Role } from '@app/auth/core';
import { User, UserService } from '@app/user-management/core';

@Component({
  selector: 'rolx-user-table',
  templateUrl: './user-table.component.html',
  styleUrls: ['./user-table.component.scss'],
})
export class UserTableComponent implements OnInit {

  users: MatTableDataSource<User>;
  Role = Role;

  get displayedColumns(): string[] {
    const allColumns = ['avatar', 'fullName', 'email', 'role', 'tools'];
    if (this.authService.currentUser.role === Role.Administrator) {
      return allColumns;
    } else {
      return allColumns.slice(0, -1);
    }
  }

  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private userService: UserService,
              public authService: AuthService) { }

  ngOnInit() {
    this.userService.getAll()
      .subscribe(users => {
        this.users = new MatTableDataSource<User>(users);
        this.users.sort = this.sort;
      });
  }

}
