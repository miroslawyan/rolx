import { User } from '@app/users/core';

export class AuthenticatedUser extends User {
  bearerToken: string;
}
