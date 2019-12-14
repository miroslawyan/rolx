// -----------------------------------------------------------------------
// <copyright file="UserSetting.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolXServer.Auth.DataAccess
{
    /// <summary>
    /// The settings of a user.
    /// </summary>
    public sealed class UserSetting
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the start date this setting is applicable.
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the part-time factor.
        /// </summary>
        public double PartTimeFactor { get; set; } = 1.0;
    }
}
