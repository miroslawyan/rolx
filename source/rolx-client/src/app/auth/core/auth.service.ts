import { Injectable } from '@angular/core';
import { classToPlain, plainToClass } from 'class-transformer';
import { BehaviorSubject } from 'rxjs';
import { Approval } from './approval';
import { SignInService } from './sign-in.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private static readonly CurrentApprovalKey = 'currentApproval';

  private readonly currentApprovalSubject = new BehaviorSubject<Approval | null>(null);
  private isInitialized = false;

  currentApproval$ = this.currentApprovalSubject.asObservable();
  get currentApproval() {
    return this.currentApprovalSubject.value;
  }

  constructor(private signInService: SignInService) {
    console.log('--- AuthService.ctor()');
  }

  private static LoadCurrentApproval(): Approval | null {
    const approvalJson = localStorage.getItem(AuthService.CurrentApprovalKey);
    if (!approvalJson) {
      return null;
    }

    const approvalPlain = JSON.parse(approvalJson);
    return plainToClass(Approval, approvalPlain);
  }

  private static StoreCurrentApproval(approval: Approval) {
    localStorage.setItem(AuthService.CurrentApprovalKey, JSON.stringify(classToPlain(approval)));
  }

  private static ClearCurrentApproval() {
    localStorage.removeItem(AuthService.CurrentApprovalKey);
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
      approval = await this.signInService.extend().toPromise();
    }

    AuthService.StoreCurrentApproval(approval);
    this.currentApprovalSubject.next(approval);

    console.log('--- AuthService.initialize() done');
  }

  async signIn(googleIdToken: string): Promise<void> {
    const approval = await this.signInService.signIn({
      googleIdToken,
    }).toPromise();

    AuthService.StoreCurrentApproval(approval);
    this.currentApprovalSubject.next(approval);
  }

  signOut() {
    AuthService.ClearCurrentApproval();
    this.currentApprovalSubject.next(null);
  }
}
