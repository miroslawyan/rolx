import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router,
              private authService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const approval = this.authService.currentApproval;
    if (approval) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${approval.bearerToken}`,
        },
      });

      return next.handle(request).pipe(
        catchError( e => { this.handleError(e); throw e; }),
      );
    }

    return next.handle(request);
  }

  private handleError(error: any) {
    const state = this.router.routerState.snapshot;

    if (error instanceof HttpErrorResponse) {
      if (error.status === 401) {
        this.authService.signOut();

        // noinspection JSIgnoredPromiseFromCall
        this.router.navigate(['/sign-in'], { queryParams: { forwardRoute: state.url } });
      }
    }
  }
}
