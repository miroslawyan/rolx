
export enum SignInState {
  Unknown,
  Known,
  Authenticated,
  Authorized,
}

export class User {
  state = SignInState.Unknown;
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  avatarUrl: string;
  googleIdToken: string;

  static fromGoogleUser(googleUser: gapi.auth2.GoogleUser): User {
    const user = new User();

    const profile = googleUser.getBasicProfile();
    if (profile == null) {
      return user;
    }

    user.state = googleUser.isSignedIn() ? SignInState.Authenticated : SignInState.Known;
    user.firstName = profile.getGivenName();
    user.lastName = profile.getFamilyName();
    user.email = profile.getEmail();
    user.avatarUrl = profile.getImageUrl();
    user.googleIdToken = googleUser.getAuthResponse().id_token;

    return user;
  }
}
