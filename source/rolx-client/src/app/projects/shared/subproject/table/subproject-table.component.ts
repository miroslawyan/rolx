import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { assertDefined } from '@app/core/util/utils';
import { Subproject } from '@app/projects/core/subproject';
import { SubprojectFilterService } from '@app/projects/core/subproject-filter.service';
import { SubprojectService } from '@app/projects/core/subproject.service';
import * as moment from 'moment';

@Component({
  selector: 'rolx-subproject-table',
  templateUrl: './subproject-table.component.html',
  styleUrls: ['./subproject-table.component.scss'],
})
export class SubprojectTableComponent implements OnInit {
  private subprojects: Subproject[] = [];

  readonly displayedColumns: string[] = [
    'fullNumber',
    'customerName',
    'projectName',
    'name',
    'tools',
  ];
  readonly dataSource = new MatTableDataSource<Subproject>();

  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  constructor(
    private readonly subprojectService: SubprojectService,
    public readonly filterService: SubprojectFilterService,
  ) {}

  ngOnInit() {
    assertDefined(this, 'sort');

    this.subprojectService.getAll().subscribe((subprojects) => {
      this.subprojects = subprojects;
      this.update(this.filterService.showClosed);
    });

    this.dataSource.sort = this.sort;
    this.dataSource.filter = this.filterService.filterText.toLowerCase();
  }

  applyFilter(value: string) {
    this.filterService.filterText = value;
    this.dataSource.filter = this.filterService.filterText.toLowerCase();
  }

  update(showClosed: boolean) {
    const deadline = moment().subtract(3, 'month');
    this.dataSource.data = showClosed
      ? this.subprojects
      : this.subprojects.filter((subproject) => subproject.endDate?.isAfter(deadline) ?? true);
  }
}
