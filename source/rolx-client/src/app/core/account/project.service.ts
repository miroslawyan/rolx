import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, mapTo } from 'rxjs/operators';

import { environment } from '@env/environment';
import { Project, ProjectData } from './project';

const ProjectUrl = environment.apiBaseUrl + '/v1/project';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private httpClient: HttpClient) { }

  private static UrlWithId(id: number) {
    return ProjectUrl + '/' + id;
  }

  getAll(): Observable<Project[]> {
    return this.httpClient.get<ProjectData[]>(ProjectUrl).pipe(
      map(data => data.map(d => new Project(d)))
    );
  }

  getById(id: number): Observable<Project> {
    return this.httpClient.get<ProjectData>(ProjectService.UrlWithId(id)).pipe(
      map(data => new Project(data))
    );
  }

  create(project: Project): Observable<Project> {
    return this.httpClient.post<ProjectData>(ProjectUrl, project.raw).pipe(
      map(data => new Project(data))
    );
  }

  update(project: Project): Observable<Project> {
    return this.httpClient.put(ProjectService.UrlWithId(project.raw.id), project.raw).pipe(
      mapTo(project)
    );
  }

}
