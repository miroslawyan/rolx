// -----------------------------------------------------------------------
// <copyright file="Record.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace RolXServer.WorkRecord.WebApi.Resource
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
        /// Gets or sets the type of the day.
        /// </summary>
        public DayType DayType { get; set; }

        /// <summary>
        /// Gets or sets the name of the day.
        /// </summary>
        public string DayName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the nominal work-time in hours.
        /// </summary>
        public double NominalWorkTimeHours { get; set; }
    }
}
