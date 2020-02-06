import { Role } from '@app/auth/core';
import { UserData } from '@app/auth/core/user.data';

export class User implements UserData {
  id: string;
  googleId: string;
  firstName: string;
  lastName: string;
  email: string;
  avatarUrl: string;
  role: Role;

  get fullName(): string {
    return `${this.firstName} ${this.lastName}`;
  }
}
