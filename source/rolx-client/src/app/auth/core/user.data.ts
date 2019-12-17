import { Role } from '@app/auth/core/role';

export interface UserData {
  id: string;
  googleId: string;
  firstName: string;
  lastName: string;
  email: string;
  avatarUrl: string;
  role: Role;
}
