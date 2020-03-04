// -----------------------------------------------------------------------
// <copyright file="Balance.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Records.WebApi.Resource
{
    /// <summary>
    /// The balance.
    /// </summary>
    public sealed class Balance
    {
        /// <summary>
        /// Gets or sets the date this instance is by.
        /// </summary>
        public string ByDate { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the overtime in seconds.
        /// </summary>
        public long Overtime { get; set; }

        /// <summary>
        /// Gets or sets the available vacation in days.
        /// </summary>
        public double VacationAvailableDays { get; set; }

        /// <summary>
        /// Gets or sets the planned vacation days.
        /// </summary>
        public double VacationPlannedDays { get; set; }
    }
}
