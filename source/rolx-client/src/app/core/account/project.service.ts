import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ErrorResponse } from '@app/core/error';
import { environment } from '@env/environment';
import { Observable, throwError } from 'rxjs';
import { catchError, map, mapTo } from 'rxjs/operators';
import { Project, ProjectData } from './project';

const ProjectUrl = environment.apiBaseUrl + '/v1/project';

@Injectable({
  providedIn: 'root',
})
export class ProjectService {

  constructor(private httpClient: HttpClient) { }

  private static UrlWithId(id: number) {
    return ProjectUrl + '/' + id;
  }

  getAll(): Observable<Project[]> {
    return this.httpClient.get<ProjectData[]>(ProjectUrl).pipe(
      map(data => data.map(d => Project.fromData(d))),
    );
  }

  getById(id: number): Observable<Project> {
    return this.httpClient.get<ProjectData>(ProjectService.UrlWithId(id)).pipe(
      map(data => Project.fromData(data)),
    );
  }

  create(project: Project): Observable<Project> {
    return this.httpClient.post<ProjectData>(ProjectUrl, project.toData()).pipe(
      map(data => Project.fromData(data)),
      catchError(e => throwError(new ErrorResponse(e))),
    );
  }

  update(project: Project): Observable<Project> {
    return this.httpClient.put(ProjectService.UrlWithId(project.id), project.toData()).pipe(
      mapTo(project),
      catchError(e => throwError(new ErrorResponse(e))),
    );
  }

}
