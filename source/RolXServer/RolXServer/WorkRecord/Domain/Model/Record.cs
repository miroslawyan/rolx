// -----------------------------------------------------------------------
// <copyright file="Record.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace RolXServer.WorkRecord.Domain.Model
{
    /// <summary>
    /// A record for a day.
    /// </summary>
    public sealed class Record
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the type of the day.
        /// </summary>
        public DayType DayType { get; set; }

        /// <summary>
        /// Gets or sets the name of the day.
        /// </summary>
        public string DayName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the nominal work-time.
        /// </summary>
        public TimeSpan NominalWorkTime { get; set; }

        /// <summary>
        /// Gets or sets the entries.
        /// </summary>
        public List<DataAccess.RecordEntry> Entries { get; set; } = new List<DataAccess.RecordEntry>();
    }
}
