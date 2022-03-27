import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from '@app/auth/core/auth.service';
import { assertDefined } from '@app/core/util/utils';
import { Role } from '@app/users/core/role';
import { User } from '@app/users/core/user';
import { UserService } from '@app/users/core/user.service';
import * as moment from 'moment';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'rolx-user-table',
  templateUrl: './user-table.component.html',
  styleUrls: ['./user-table.component.scss'],
})
export class UserTableComponent implements OnInit {
  private users: User[] = [];

  readonly dataSource = new MatTableDataSource<User>();
  readonly Role = Role;
  readonly displayedColumns = [
    'avatar',
    'fullName',
    'email',
    'role',
    'entryDate',
    'leavingDate',
    this.authService.currentApprovalOrError.isAdministrator ? 'admin-tools' : 'tools',
  ];

  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  constructor(private userService: UserService, public authService: AuthService) {}

  ngOnInit() {
    assertDefined(this, 'sort');

    this.dataSource.sort = this.sort;

    this.userService
      .getAll()
      .pipe(tap((users) => (this.users = users)))
      .subscribe(() => this.update(false));
  }

  update(showFormerUsers: boolean) {
    const deadline = moment().subtract(3, 'month');
    this.dataSource.data = showFormerUsers
      ? this.users
      : this.users.filter((u) => u.leavingDate?.isAfter(deadline) ?? true);
  }
}
