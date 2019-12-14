// -----------------------------------------------------------------------
// <copyright file="RecordEntry.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

using RolXServer.Account.DataAccess;

namespace RolXServer.WorkRecord.DataAccess
{
    /// <summary>
    /// An entry in a <see cref="Record"/>.
    /// </summary>
    public sealed class RecordEntry
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the record identifier.
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public TimeSpan Duration { get; set; }

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
