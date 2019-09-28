import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthService } from './auth.service';
import { SignInState } from './sign-in-state';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router,
              private authService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const currentUser = this.authService.currentUser;
    if (currentUser.state === SignInState.SignedIn && currentUser.bearerToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.bearerToken}`
        }
      });

      return next.handle(request).pipe(
        catchError( e => { this.handleError(e); throw e; })
      );
    }

    return next.handle(request);
  }

  private handleError(error: any) {
    const state = this.router.routerState.snapshot;

    if (error instanceof HttpErrorResponse) {
      if (error.status === 401) {
        this.authService.signOut()
          .subscribe(
            () => this.router.navigate(['/sign-in'], { queryParams: { forwardRoute: state.url } }));
      }
    }
  }
}
