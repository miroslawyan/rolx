import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ErrorResponse } from '@app/core/error';
import { environment } from '@env/environment';
import { Observable, throwError } from 'rxjs';
import { catchError, map, mapTo } from 'rxjs/operators';
import { Project } from './project';

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
    return this.httpClient.get<any[]>(ProjectUrl).pipe(
      map(data => data.map(d => Project.fromJson(d))),
    );
  }

  getById(id: number): Observable<Project> {
    return this.httpClient.get(ProjectService.UrlWithId(id)).pipe(
      map(data => Project.fromJson(data)),
    );
  }

  create(project: Project): Observable<Project> {
    return this.httpClient.post(ProjectUrl, project.toJson()).pipe(
      map(data => Project.fromJson(data)),
      catchError(e => throwError(new ErrorResponse(e))),
    );
  }

  update(project: Project): Observable<Project> {
    return this.httpClient.put(ProjectService.UrlWithId(project.id), project.toJson()).pipe(
      mapTo(project),
      catchError(e => throwError(new ErrorResponse(e))),
    );
  }

}
