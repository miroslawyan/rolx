import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from '@app/auth/core/auth.service';
import { SortService } from '@app/core/persistence/sort.service';
import { assertDefined } from '@app/core/util/utils';
import { Role } from '@app/users/core/role';
import { User } from '@app/users/core/user';
import { UserFilterService } from '@app/users/core/user-filter.service';
import { UserService } from '@app/users/core/user.service';
import * as moment from 'moment';

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
    'tools',
  ];

  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  constructor(
    private readonly userService: UserService,
    private readonly sortService: SortService,
    public readonly authService: AuthService,
    public readonly filterService: UserFilterService,
  ) {}

  ngOnInit() {
    assertDefined(this, 'sort');

    this.userService.getAll().subscribe((users) => {
      this.users = users;
      this.update(this.filterService.showLefties);
    });

    this.dataSource.sort = this.sort;
    this.dataSource.filterPredicate = UserFilterService.Predicate;
    this.dataSource.filter = this.filterService.filterText.toLowerCase();

    this.sort.sort(
      this.sortService.get('User', {
        id: this.displayedColumns[1],
        start: 'asc',
        disableClear: true,
      }),
    );
    this.sort.sortChange.subscribe((sort) => this.sortService.set('User', sort));
  }

  tpd(user: User): User {
    return user;
  }

  applyFilter(value: string) {
    this.filterService.filterText = value;
    this.dataSource.filter = this.filterService.filterText.toLowerCase();
  }

  update(showLefties: boolean) {
    const deadline = moment().subtract(3, 'month');
    this.dataSource.data = showLefties
      ? this.users
      : this.users.filter((user) => user.leavingDate?.isAfter(deadline) ?? true);
  }
}
