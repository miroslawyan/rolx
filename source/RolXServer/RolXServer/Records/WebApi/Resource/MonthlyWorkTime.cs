// -----------------------------------------------------------------------
// <copyright file="MonthlyWorkTime.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Records.WebApi.Resource
{
    /// <summary>
    /// An entry in a <see cref="YearInfo"/>.
    /// </summary>
    public sealed class MonthlyWorkTime
    {
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        public string Month { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the work days.
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// Gets or sets the work hours in seconds.
        /// </summary>
        public int Hours { get; set; }
    }
}
