// -----------------------------------------------------------------------
// <copyright file="BearerToken.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace RolXServer.Auth.Domain.Detail
{
    /// <summary>
    /// The bearer token.
    /// </summary>
    internal sealed class BearerToken
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date/time this token expires.
        /// </summary>
        public DateTime Expires { get; set; }
    }
}
