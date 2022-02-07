import { Injectable } from '@angular/core';
import { Role } from '@app/auth/core/role';
import { instanceToPlain, plainToInstance } from 'class-transformer';
import { BehaviorSubject, lastValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';

import { Approval } from './approval';
import { SignInService } from './sign-in.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private static readonly CurrentApprovalKey = 'currentApproval';

  private readonly currentApprovalSubject = new BehaviorSubject<Approval | null>(null);
  private isInitialized = false;

  readonly currentApproval$ = this.currentApprovalSubject.asObservable();
  get currentApproval() {
    return this.currentApprovalSubject.value;
  }

  readonly currentIsSupervisor$ = this.currentApproval$.pipe(
    map((a) => (a?.user.role ?? Role.User) >= Role.Supervisor),
  );

  constructor(private signInService: SignInService) {
    console.log('--- AuthService.ctor()');
  }

  async initialize(): Promise<void> {
    if (this.isInitialized) {
      return;
    }

    console.log('--- AuthService.initialize()');
    this.isInitialized = true;

    let approval = AuthService.LoadCurrentApproval();

    if (!approval || approval.isExpired) {
      AuthService.ClearCurrentApproval();
      this.currentApprovalSubject.next(null);

      console.log('--- AuthService.initialize() done');
      return;
    }

    if (approval.willExpireSoon) {
      approval = await lastValueFrom(this.signInService.extend());
    }

    AuthService.StoreCurrentApproval(approval);
    this.currentApprovalSubject.next(approval);

    console.log('--- AuthService.initialize() done');
  }

  async signIn(googleIdToken: string): Promise<void> {
    const approval = await lastValueFrom(
      this.signInService.signIn({
        googleIdToken,
      }),
    );

    AuthService.StoreCurrentApproval(approval);
    this.currentApprovalSubject.next(approval);
  }

  signOut() {
    AuthService.ClearCurrentApproval();
    this.currentApprovalSubject.next(null);
  }

  private static LoadCurrentApproval(): Approval | null {
    const approvalJson = localStorage.getItem(AuthService.CurrentApprovalKey);
    if (!approvalJson) {
      return null;
    }

    const approvalPlain = JSON.parse(approvalJson);
    const approval = plainToInstance(Approval, approvalPlain);

    try {
      approval.validateModel();
    } catch (e) {
      console.warn('failed to restore approval', e);
      return null;
    }

    return approval;
  }

  private static StoreCurrentApproval(approval: Approval) {
    localStorage.setItem(AuthService.CurrentApprovalKey, JSON.stringify(instanceToPlain(approval)));
  }

  private static ClearCurrentApproval() {
    localStorage.removeItem(AuthService.CurrentApprovalKey);
  }
}
