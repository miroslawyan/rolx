import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';

import { PendingRequestService } from './pending-request.service';

@Injectable()
export class PendingRequestInterceptor implements HttpInterceptor {
  constructor(private pendingRequestService: PendingRequestService, private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.pendingRequestService.requestStarted();
    let interceptedNext = next
      .handle(request)
      .pipe(finalize(() => this.pendingRequestService.requestFinished()));

    if (request.method === 'GET') {
      interceptedNext = interceptedNext.pipe(catchError((err) => this.handleServerError(err)));
    }

    return interceptedNext;
  }

  private handleServerError(err: any) {
    // noinspection JSIgnoredPromiseFromCall
    this.router.navigate(['/server-error']);

    return throwError(() => err);
  }
}
