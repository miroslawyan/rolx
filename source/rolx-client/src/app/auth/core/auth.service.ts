import { Injectable } from '@angular/core';
import { instanceToPlain, plainToInstance } from 'class-transformer';
import { lastValueFrom } from 'rxjs';

import { Approval } from './approval';
import { SignInService } from './sign-in.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private static readonly CurrentApprovalKey = 'currentApproval';

  private _currentApproval?: Approval;
  private isInitialized = false;

  get currentApproval() {
    return this._currentApproval;
  }

  get currentApprovalOrError() {
    if (this._currentApproval == null) {
      throw new Error('Not logged in properly');
    }

    return this._currentApproval;
  }

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
      this._currentApproval = undefined;

      console.log('--- AuthService.initialize() done');
      return;
    }

    if (approval.willExpireSoon) {
      approval = await lastValueFrom(this.signInService.extend());
    }

    AuthService.StoreCurrentApproval(approval);
    this._currentApproval = approval;

    console.log('--- AuthService.initialize() done');
  }

  async signIn(googleIdToken: string): Promise<void> {
    const approval = await lastValueFrom(
      this.signInService.signIn({
        googleIdToken,
      }),
    );

    AuthService.StoreCurrentApproval(approval);
    this._currentApproval = approval;
  }

  signOut() {
    AuthService.ClearCurrentApproval();
    this._currentApproval = undefined;
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
