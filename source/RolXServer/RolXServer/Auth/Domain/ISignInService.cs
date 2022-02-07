// -----------------------------------------------------------------------
// <copyright file="ISignInService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Auth.Domain.Model;

namespace RolXServer.Auth.Domain;

/// <summary>
/// Service for signing users in.
/// </summary>
public interface ISignInService
{
    /// <summary>
    /// Gets the sign-in information.
    /// </summary>
    /// <returns>The sign-in information.</returns>
    Task<Info> GetInfo();

    /// <summary>
    /// Authenticates with the specified google identifier token.
    /// </summary>
    /// <param name="signInData">The sign in data.</param>
    /// <returns>
    /// The approval or <c>null</c> if authentication failed.
    /// </returns>
    Task<Approval?> Authenticate(SignInData signInData);

    /// <summary>
    /// Extends the authentication for user with the specified identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>
    /// The approval or <c>null</c> if authentication failed.
    /// </returns>
    Task<Approval?> Extend(Guid userId);
}
