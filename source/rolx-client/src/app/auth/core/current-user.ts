import { AuthenticatedUserData } from './authenticated-user.data';
import { Role } from './role';
import { SignInState } from './sign-in-state';
import { SignInData } from './sign-in.data';

export class CurrentUser implements AuthenticatedUserData, SignInData {
  state = SignInState.Unknown;

  id: string;
  googleId: string;
  firstName: string;
  lastName: string;
  email: string;
  avatarUrl: string;
  role: Role;
  bearerToken: string;

  googleIdToken: string;

  static fromGoogleUser(googleUser: gapi.auth2.GoogleUser): CurrentUser {
    const user = new CurrentUser();

    const profile = googleUser.getBasicProfile();
    if (profile == null) {
      return user;
    }

    user.state = googleUser.isSignedIn() ? SignInState.Authenticated : SignInState.Known;
    user.firstName = profile.getGivenName();
    user.lastName = profile.getFamilyName();
    user.email = profile.getEmail();
    user.avatarUrl = profile.getImageUrl();
    user.googleId = profile.getId();
    user.googleIdToken = googleUser.getAuthResponse().id_token;

    return user;
  }

  static fromAuthenticatedUser(authenticatedUser: AuthenticatedUserData): CurrentUser {
    const currentUser = Object.assign(new CurrentUser(), authenticatedUser);
    currentUser.state = SignInState.SignedIn;
    return currentUser;
  }
}
