// -----------------------------------------------------------------------
// <copyright file="Settings.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace RolXServer.Auth
{
    /// <summary>
    /// The settings for the Auth package.
    /// </summary>
    public sealed class Settings
    {
        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        public string Secret { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the token expiration duration.
        /// </summary>
        public TimeSpan TokenExpiration { get; set; } = TimeSpan.FromDays(1);

        /// <summary>
        /// Gets or sets the google client identifier.
        /// </summary>
        public string GoogleClientId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the white-list of allowed google hosted domains.
        /// </summary>
        public string[] GoogleHostedDomainWhitelist { get; set; } = new string[0];
    }
}
