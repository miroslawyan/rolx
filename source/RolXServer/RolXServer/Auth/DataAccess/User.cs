// -----------------------------------------------------------------------
// <copyright file="User.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

using RolXServer.Common.DataAccess;

namespace RolXServer.Auth.DataAccess
{
    /// <summary>
    /// Represents a user of RolX.
    /// </summary>
    public class User : IAggregateRoot
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the google identifier.
        /// </summary>
        public string GoogleId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the avatar URL.
        /// </summary>
        public string AvatarUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public Role Role { get; set; }
    }
}
