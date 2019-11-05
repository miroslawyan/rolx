import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';
import { Project, ProjectService } from '@app/core/account';

@Component({
  selector: 'rolx-project-table',
  templateUrl: './project-table.component.html',
  styleUrls: ['./project-table.component.scss'],
})
export class ProjectTableComponent implements OnInit {

  displayedColumns: string[] = ['number', 'customerName', 'name', 'openUntil', 'tools'];
  projects: MatTableDataSource<Project>;

  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private projectService: ProjectService) { }

  ngOnInit() {
    this.projectService.getAll()
      .subscribe(projects => {
        this.projects = new MatTableDataSource<Project>(projects);
        this.projects.sort = this.sort;
      });
  }

}
