// -----------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using RolXServer.Auth.Domain.Model;

namespace RolXServer.Auth.Domain
{
    /// <summary>
    /// Provides access to users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates with the specified google identifier token.
        /// </summary>
        /// <param name="googleIdToken">The google identifier token.</param>
        /// <returns>The authenticated user.</returns>
        Task<AuthenticatedUser> Authenticate(string googleIdToken);
    }
}
