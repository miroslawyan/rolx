import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ErrorResponse } from '@app/core/error/error-response';
import { mapPlainToInstance, mapPlainToInstances } from '@app/core/util/operators';
import { environment } from '@env/environment';
import { instanceToPlain } from 'class-transformer';
import { Observable, tap, throwError } from 'rxjs';
import { catchError, mapTo } from 'rxjs/operators';

import { Project } from './project';

const ProjectUrl = environment.apiBaseUrl + '/v1/project';

@Injectable({
  providedIn: 'root',
})
export class ProjectService {
  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<Project[]> {
    return this.httpClient.get<object[]>(ProjectUrl).pipe(
      mapPlainToInstances(Project),
      tap((ps) => ps.forEach((p) => p.validateModel())),
    );
  }

  getById(id: number): Observable<Project> {
    return this.httpClient.get(ProjectService.UrlWithId(id)).pipe(
      mapPlainToInstance(Project),
      tap((p) => p.validateModel()),
    );
  }

  create(project: Project): Observable<Project> {
    return this.httpClient.post(ProjectUrl, instanceToPlain(project)).pipe(
      mapPlainToInstance(Project),
      tap((p) => p.validateModel()),
      catchError((e) => throwError(new ErrorResponse(e))),
    );
  }

  update(project: Project): Observable<Project> {
    return this.httpClient.put(ProjectService.UrlWithId(project.id), instanceToPlain(project)).pipe(
      mapTo(project),
      catchError((e) => throwError(new ErrorResponse(e))),
    );
  }

  private static UrlWithId(id: number) {
    return ProjectUrl + '/' + id;
  }
}
