// -----------------------------------------------------------------------
// <copyright file="User.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolXServer.Users.DataAccess
{
    /// <summary>
    /// Represents a user of RolX.
    /// </summary>
    public class User
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "EF cannot map System.Uri")]
        public string AvatarUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Gets or sets the entry date.
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the leaving date (inclusive).
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? LeavingDate { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        public List<UserSetting> Settings { get; set; } = new List<UserSetting>();
    }
}
