// -----------------------------------------------------------------------
// <copyright file="SignInData.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Auth.Domain.Model
{
    /// <summary>
    /// The data required for signing in.
    /// </summary>
    public sealed class SignInData
    {
        /// <summary>
        /// Gets or sets the google identifier token.
        /// </summary>
        public string GoogleIdToken { get; set; } = string.Empty;
    }
}
