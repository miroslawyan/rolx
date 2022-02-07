import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '@app/users/core/user';
import { UserService } from '@app/users/core/user.service';
import { Observable, throwError } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'rolx-user-edit-page',
  templateUrl: './user-edit-page.component.html',
  styleUrls: ['./user-edit-page.component.scss'],
})
export class UserEditPageComponent {
  readonly user$: Observable<User> = this.route.paramMap.pipe(
    map((params) => params.get('id') ?? 'definitely-not-a-user-id'),
    switchMap((id) => this.userService.getById(id)),
    catchError((e) => {
      if (e.status === 404) {
        // noinspection JSIgnoredPromiseFromCall
        this.router.navigate(['/four-oh-four']);
      }

      return throwError(e);
    }),
  );

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
  ) {}
}
