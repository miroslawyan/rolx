import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { SortService } from '@app/core/persistence/sort.service';
import { assertDefined } from '@app/core/util/utils';
import { SubprojectFilterService } from '@app/projects/core/subproject-filter.service';
import { SubprojectShallow } from '@app/projects/core/subproject-shallow';
import { SubprojectService } from '@app/projects/core/subproject.service';

@Component({
  selector: 'rolx-subproject-table',
  templateUrl: './subproject-table.component.html',
  styleUrls: ['./subproject-table.component.scss'],
})
export class SubprojectTableComponent implements OnInit {
  private readonly isClosedColumn = 'isClosed';
  private readonly columns = [
    'fullNumber',
    'customerName',
    'projectName',
    'name',
    'managerName',
    this.isClosedColumn,
    'tools',
  ];

  private subprojects: SubprojectShallow[] = [];

  readonly dataSource = new MatTableDataSource<SubprojectShallow>();
  displayedColumns = this.columns;

  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  constructor(
    private readonly subprojectService: SubprojectService,
    private readonly sortService: SortService,
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

    this.sort.sort(
      this.sortService.get('Subproject', {
        id: this.displayedColumns[0],
        start: 'asc',
        disableClear: true,
      }),
    );
    this.sort.sortChange.subscribe((sort) => this.sortService.set('Subproject', sort));
  }

  tpd(subproject: SubprojectShallow): SubprojectShallow {
    return subproject;
  }

  applyFilter(value: string) {
    this.filterService.filterText = value;
    this.dataSource.filter = this.filterService.filterText.toLowerCase();
  }

  update(showClosed: boolean) {
    this.displayedColumns = showClosed
      ? this.columns
      : this.columns.filter((c) => c !== this.isClosedColumn);

    this.dataSource.data = showClosed
      ? this.subprojects
      : this.subprojects.filter((subproject) => !subproject.isClosed);
  }
}
