// -----------------------------------------------------------------------
// <copyright file="ISignInService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using RolXServer.Auth.Domain.Model;

namespace RolXServer.Auth.Domain
{
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
        /// <param name="googleIdToken">The google identifier token.</param>
        /// <returns>The authenticated user.</returns>
        Task<AuthenticatedUser?> Authenticate(string googleIdToken);

        /// <summary>
        /// Extends the authentication for user with the specified identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The authenticated user.</returns>
        Task<AuthenticatedUser?> Extend(Guid userId);
    }
}
