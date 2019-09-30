// -----------------------------------------------------------------------
// <copyright file="RecordService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RolXServer.WorkRecord.Domain.Model;

namespace RolXServer.WorkRecord.Domain.Detail
{
    /// <summary>
    /// Provides access to records.
    /// </summary>
    public sealed class RecordService : IRecordService
    {
        /// <summary>
        /// Gets all records of the specified month.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <returns>
        /// The records.
        /// </returns>
        public Task<IEnumerable<Record>> GetAllOfMonth(DateTime month)
        {
            var result = AllDaysOfSameMonth(month)
                .Select(d => new Record { Date = d });

            return Task.FromResult(result);
        }

        private static IEnumerable<DateTime> AllDaysOfSameMonth(DateTime month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(month.Year, month.Month))
                .Select(d => new DateTime(month.Year, month.Month, d));
        }
    }
}
