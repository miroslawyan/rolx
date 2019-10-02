// -----------------------------------------------------------------------
// <copyright file="DayType.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.WorkRecord
{
    /// <summary>
    /// The types of a day.
    /// </summary>
    public enum DayType
    {
        /// <summary>
        /// A plain old, boring workday.
        /// </summary>
        Workday,

        /// <summary>
        /// Hurray, its weekend!
        /// </summary>
        Weekend,

        /// <summary>
        /// A public holiday.
        /// </summary>
        Holiday,
    }
}
