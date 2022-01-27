// -----------------------------------------------------------------------
// <copyright file="YearInfoService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

using Microsoft.Extensions.Options;
using RolXServer.Common.Util;
using RolXServer.Records.Domain.Detail.Balances;
using RolXServer.Records.Domain.Model;

namespace RolXServer.Records.Domain.Detail
{
    /// <summary>
    /// Provides access to year info.
    /// </summary>
    public class YearInfoService : IYearInfoService
    {
        private readonly Settings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="YearInfoService" /> class.
        /// </summary>
        /// <param name="settingsAccessor">The settings accessor.</param>
        public YearInfoService(
            IOptions<Settings> settingsAccessor)
        {
            this.settings = settingsAccessor.Value;
        }

        /// <inheritdoc/>
        public YearInfo GetFor(int year)
        {
            var range = DateRange.ForYear(year);
            var dayInfos = range.DayInfos(this.settings.NominalWorkTimePerDay);

            var holidays = dayInfos.Where(c => c.DayType == DayType.Holiday)
                .Select(d => new Holiday
                {
                    Name = d.DayName,
                    Date = d.Date,
                }).ToList();

            var monthlyWorkTimes = dayInfos.GroupBy(a => a.Date.Month)
                .Select(d => new MonthlyWorkTime
                {
                    Month = d.First().Date,
                    Days = d.Where(m => m.DayType == DayType.Workday).Count(),
                    Hours = this.settings.NominalWorkTimePerDay *
                    d.Where(m => m.DayType == DayType.Workday).Count(),
                }).ToList();

            return new YearInfo
            {
                Holidays = holidays,
                MonthlyWorkTimes = monthlyWorkTimes,
            };
        }
    }
}
