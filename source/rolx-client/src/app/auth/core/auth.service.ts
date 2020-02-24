import { Injectable } from '@angular/core';
import { AuthenticatedUser } from '@app/auth/core/authenticated.user';
import { classToPlain, plainToClass } from 'class-transformer';
import { BehaviorSubject } from 'rxjs';
import { SignInService } from './sign-in.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private static readonly CurrentUserKey = 'currentUser';

  private readonly currentUserSubject = new BehaviorSubject<AuthenticatedUser | null>(null);
  private isInitialized = false;

  currentUser$ = this.currentUserSubject.asObservable();
  get currentUser() {
    return this.currentUserSubject.value;
  }

  constructor(private signInService: SignInService) {
    console.log('--- AuthService.ctor()');
  }

  private static LoadCurrentUser(): AuthenticatedUser | null {
    const userJson = localStorage.getItem(AuthService.CurrentUserKey);
    if (!userJson) {
      return null;
    }

    const userPlain = JSON.parse(userJson);
    return plainToClass(AuthenticatedUser, userPlain);
  }

  private static StoreCurrentUser(user: AuthenticatedUser) {
    localStorage.setItem(AuthService.CurrentUserKey, JSON.stringify(classToPlain(user)));
  }

  private static ClearCurrentUser() {
    localStorage.removeItem(AuthService.CurrentUserKey);
  }

  async initialize(): Promise<void> {
    if (this.isInitialized) {
      return;
    }

    console.log('--- AuthService.initialize()');
    this.isInitialized = true;

    let currentUser = AuthService.LoadCurrentUser();

    if (!currentUser || currentUser.isExpired) {
      AuthService.ClearCurrentUser();
      this.currentUserSubject.next(null);

      console.log('--- AuthService.initialize() done');
      return;
    }

    if (currentUser.willExpireSoon) {
      currentUser = await this.signInService.extend().toPromise();
    }

    AuthService.StoreCurrentUser(currentUser);
    this.currentUserSubject.next(currentUser);

    console.log('--- AuthService.initialize() done');
  }

  async signIn(googleIdToken: string): Promise<void> {
    const currentUser = await this.signInService.signIn({
      googleIdToken,
    }).toPromise();

    AuthService.StoreCurrentUser(currentUser);
    this.currentUserSubject.next(currentUser);
  }

  signOut() {
    AuthService.ClearCurrentUser();
    this.currentUserSubject.next(null);
  }
}
