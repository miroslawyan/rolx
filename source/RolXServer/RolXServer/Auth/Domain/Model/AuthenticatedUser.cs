// -----------------------------------------------------------------------
// <copyright file="AuthenticatedUser.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

using RolXServer.Users.DataAccess;

namespace RolXServer.Auth.Domain.Model
{
    /// <summary>
    /// Represents an authenticated user.
    /// </summary>
    public class AuthenticatedUser : User
    {
        /// <summary>
        /// Gets or sets the bearer token.
        /// </summary>
        public string BearerToken { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date/time this token expires.
        /// </summary>
        public DateTime Expires { get; set; }
    }
}
