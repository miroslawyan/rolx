// -----------------------------------------------------------------------
// <copyright file="UserBalanceCorrection.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace RolXServer.Users.DataAccess
{
    /// <summary>
    /// A correction on the balance of a user.
    /// </summary>
    public sealed class UserBalanceCorrection
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the date/time.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the overtime correction.
        /// </summary>
        public TimeSpan Overtime { get; set; }

        /// <summary>
        /// Gets or sets the vacation correction.
        /// </summary>
        public TimeSpan Vacation { get; set; }
    }
}
