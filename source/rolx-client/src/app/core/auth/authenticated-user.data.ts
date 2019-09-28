import { UserData } from '@app/core/auth/user.data';

export interface AuthenticatedUserData extends UserData {
  bearerToken: string;
}
