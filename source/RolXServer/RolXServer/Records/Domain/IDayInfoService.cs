// -----------------------------------------------------------------------
// <copyright file="IDayInfoService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using RolXServer.Common.Util;
using RolXServer.Records.Domain.Model;
using RolXServer.Users.DataAccess;

namespace RolXServer.Records.Domain
{
    /// <summary>
    /// Provides informations on days.
    /// </summary>
    public interface IDayInfoService
    {
        /// <summary>
        /// Gets the day-information for the specified user in the specified range.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="range">The range.</param>
        /// <returns>
        /// The informations.
        /// </returns>
        IEnumerable<DayInfo> Get(User user, DateRange range);

        /// <summary>
        /// Gets the day-information for the specified user for all days until the specified date.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="untilDate">The until date (exclusive).</param>
        /// <returns>
        /// The informations.
        /// </returns>
        IEnumerable<DayInfo> Get(User user, DateTime untilDate)
        {
            if (!user.EntryDate.HasValue)
            {
                throw new ArgumentException("User must have an entry date", nameof(user));
            }

            return this.Get(user, new DateRange(user.EntryDate.Value, untilDate));
        }

        /// <summary>
        /// Gets the nominal work time for the specified user for all days until the specified date.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="untilDate">The until date (exclusive).</param>
        /// <returns>The nominal work time.</returns>
        TimeSpan GetNominalWorkTime(User user, DateTime untilDate)
        {
            return new TimeSpan(
                this.Get(user, untilDate).Sum(i => i.NominalWorkTime.Ticks));
        }
    }
}
