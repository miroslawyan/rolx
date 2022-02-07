import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { assertDefined } from '@app/core/util/utils';
import { Project } from '@app/projects/core/project';
import { ProjectService } from '@app/projects/core/project.service';

@Component({
  selector: 'rolx-project-table',
  templateUrl: './project-table.component.html',
  styleUrls: ['./project-table.component.scss'],
})
export class ProjectTableComponent implements OnInit {
  displayedColumns: string[] = ['number', 'name', 'tools'];
  projects = new MatTableDataSource<Project>();

  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  constructor(private projectService: ProjectService) {}

  ngOnInit() {
    assertDefined(this, 'sort');

    this.projectService.getAll().subscribe((projects) => {
      this.projects = new MatTableDataSource<Project>(projects);
      this.projects.sort = this.sort;
    });
  }
}
