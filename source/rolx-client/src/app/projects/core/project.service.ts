import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ErrorResponse } from '@app/core/error/error-response';
import { mapPlainToClass, mapPlainToClassArray } from '@app/core/util/operators';
import { environment } from '@env/environment';
import { classToPlain } from 'class-transformer';
import { Observable, throwError } from 'rxjs';
import { catchError, mapTo } from 'rxjs/operators';
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
    return this.httpClient.get(ProjectUrl).pipe(
      mapPlainToClassArray(Project),
    );
  }

  getById(id: number): Observable<Project> {
    return this.httpClient.get(ProjectService.UrlWithId(id)).pipe(
      mapPlainToClass(Project),
    );
  }

  create(project: Project): Observable<Project> {
    return this.httpClient.post(ProjectUrl, classToPlain(project)).pipe(
      mapPlainToClass(Project),
      catchError(e => throwError(new ErrorResponse(e))),
    );
  }

  update(project: Project): Observable<Project> {
    return this.httpClient.put(ProjectService.UrlWithId(project.id), classToPlain(project)).pipe(
      mapTo(project),
      catchError(e => throwError(new ErrorResponse(e))),
    );
  }

}
