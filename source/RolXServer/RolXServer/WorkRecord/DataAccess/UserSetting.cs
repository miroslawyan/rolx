// -----------------------------------------------------------------------
// <copyright file="UserSetting.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

using RolXServer.Auth.DataAccess;

namespace RolXServer.WorkRecord.DataAccess
{
    /// <summary>
    /// The settings of a user.
    /// </summary>
    public class UserSetting
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
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the part-time factor.
        /// </summary>
        public double PartTimeFactor { get; set; } = 1.0;
    }
}
