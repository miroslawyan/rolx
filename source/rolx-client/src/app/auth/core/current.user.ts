import { AuthenticatedUser } from './authenticated.user';
import { SignInData } from './sign-in.data';
import { SignInState } from './sign-in.state';

export class CurrentUser extends AuthenticatedUser implements SignInData {
  state = SignInState.Unknown;

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

  static fromAuthenticatedUser(authenticatedUser: AuthenticatedUser): CurrentUser {
    const currentUser = Object.assign(new CurrentUser(), authenticatedUser);
    currentUser.state = SignInState.SignedIn;
    return currentUser;
  }
}
