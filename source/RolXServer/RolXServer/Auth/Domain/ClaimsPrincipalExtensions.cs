// -----------------------------------------------------------------------
// <copyright file="ClaimsPrincipalExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Security.Claims;

namespace RolXServer.Auth.Domain
{
    /// <summary>
    /// Extension methods for <see cref="ClaimsPrincipal"/> instances.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Gets the user identifier of the specified principal.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns>The user identifier.</returns>
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var identity = principal.GetIdentity();
            return Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
        }

        private static ClaimsIdentity GetIdentity(this ClaimsPrincipal principal)
        {
            return (ClaimsIdentity)principal.Identity;
        }
    }
}
