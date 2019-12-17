import { UserData } from '@app/auth/core/user.data';

export interface AuthenticatedUserData extends UserData {
  bearerToken: string;
}
