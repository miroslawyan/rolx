// -----------------------------------------------------------------------
// <copyright file="RecordEntry.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Account.WebApi.Resource;

namespace RolXServer.WorkRecord.WebApi.Resource
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
        /// Gets or sets the duration in seconds.
        /// </summary>
        public long Duration { get; set; }

        /// <summary>
        /// Gets or sets the phase.
        /// </summary>
        public Phase Phase { get; set; } = new Phase();
    }
}
