import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { assertDefined } from '@app/core/util/utils';
import { Subproject } from '@app/projects/core/subproject';
import { SubprojectService } from '@app/projects/core/subproject.service';

@Component({
  selector: 'rolx-subproject-table',
  templateUrl: './subproject-table.component.html',
  styleUrls: ['./subproject-table.component.scss'],
})
export class SubprojectTableComponent implements OnInit {
  displayedColumns: string[] = ['fullNumber', 'customerName', 'projectName', 'name', 'tools'];
  subprojects = new MatTableDataSource<Subproject>();

  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  constructor(private subprojectService: SubprojectService) {}

  ngOnInit() {
    assertDefined(this, 'sort');

    this.subprojectService.getAll().subscribe((subprojects) => {
      this.subprojects = new MatTableDataSource<Subproject>(subprojects);
      this.subprojects.sort = this.sort;
    });
  }
}
