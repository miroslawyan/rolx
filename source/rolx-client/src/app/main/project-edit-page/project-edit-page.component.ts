import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Project, ProjectService } from '@app/core/account';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-project-edit-page',
  templateUrl: './project-edit-page.component.html',
  styleUrls: ['./project-edit-page.component.scss'],
})
export class ProjectEditPageComponent implements OnInit {

  project$: Observable<Project>;

  constructor(private route: ActivatedRoute, private projectService: ProjectService) { }

  ngOnInit() {
    this.project$ = this.route.paramMap.pipe(
      switchMap(params => this.initializeProject(params.get('id'))),
    );
  }

  private initializeProject(idText: string): Observable<Project> {
    return idText === 'add' ? of(new Project()) : this.projectService.getById(Number(idText));
  }

}
