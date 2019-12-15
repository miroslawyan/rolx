// -----------------------------------------------------------------------
// <copyright file="FavouritePhase.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

using RolXServer.Auth.DataAccess;

namespace RolXServer.Account.DataAccess
{
    /// <summary>
    /// A favourite phase of a user.
    /// </summary>
    public sealed class FavouritePhase
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the phase identifier.
        /// </summary>
        public int PhaseId { get; set; }

        /// <summary>
        /// Gets or sets the phase.
        /// </summary>
        public Phase? Phase { get; set; }
    }
}
