// -----------------------------------------------------------------------
// <copyright file="DayInfoService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Options;
using RolXServer.Common.Util;
using RolXServer.Records.Domain.Detail.Holiday;
using RolXServer.Records.Domain.Model;
using RolXServer.Users.DataAccess;

namespace RolXServer.Records.Domain.Detail
{
    /// <summary>
    /// Provides informations on days.
    /// </summary>
    public sealed class DayInfoService : IDayInfoService
    {
        private static readonly List<RuleBase> HolidayRules = new List<RuleBase>
        {
            new RuleAtFixedDate("Neujahr", 1, 1),
            new RuleAtFixedDate("Berchtoldstag", 1, 2),
            new RuleAtFixedDate("Tag der Arbeit", 5, 1),
            new RuleAtFixedDate("Nationalfeiertag", 8, 1),
            new RuleAtFixedDate("Weihnachten", 12, 25),
            new RuleAtFixedDate("Stephanstag", 12, 26),
            new RuleEasterBased("Karfreitag", -2),
            new RuleEasterBased("Ostern", 0),
            new RuleEasterBased("Ostermontag", 1),
            new RuleEasterBased("Auffahrt", 39),
            new RuleEasterBased("Pfingsten", 49),
            new RuleEasterBased("Pfingstmontag", 50),
        };

        private readonly Settings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="DayInfoService" /> class.
        /// </summary>
        /// <param name="settingsAccessor">The settings accessor.</param>
        public DayInfoService(IOptions<Settings> settingsAccessor)
        {
            this.settings = settingsAccessor.Value;
        }

        /// <summary>
        /// Gets the day-information for the specified user in the specified range.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="range">The range.</param>
        /// <returns>
        /// The informations.
        /// </returns>
        public IEnumerable<DayInfo> Get(User user, DateRange range)
        {
            var sortedSettings = user.Settings
                .OrderByDescending(s => s.StartDate)
                .ToList();

            return Sanitize(range, user)
                .Days
                .Select(d => new DayInfo
                {
                    Date = d,
                    NominalWorkTime = this.settings.NominalWorkTimePerDay,
                })
                .Select(d => ApplyWeekend(d))
                .Select(d => ApplyHoliday(d))
                .Select(d => ApplyPartTimeFactor(d, sortedSettings));
        }

        private static DateRange Sanitize(DateRange range, User user)
        {
            if (user.EntryDate.HasValue && user.EntryDate < range.Begin)
            {
                range = new DateRange(user.EntryDate.Value, range.End);
            }

            var leftDate = user.LeavingDate?.AddDays(1);
            if (leftDate.HasValue && leftDate > range.End)
            {
                range = new DateRange(range.Begin, leftDate.Value);
            }

            return range;
        }

        private static DayInfo ApplyWeekend(DayInfo info)
        {
            if (info.Date.DayOfWeek == DayOfWeek.Saturday || info.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                info.DayType = DayType.Weekend;
                info.NominalWorkTime = default;
            }

            return info;
        }

        private static DayInfo ApplyHoliday(DayInfo info)
        {
            var rule = HolidayRules.FirstOrDefault(r => r.IsMatching(info.Date));
            if (rule != null)
            {
                info.DayType = DayType.Holiday;
                info.DayName = rule.Name;
                info.NominalWorkTime = default;
            }

            return info;
        }

        private static DayInfo ApplyPartTimeFactor(DayInfo info, IEnumerable<UserSetting> settings)
        {
            var factor = settings
                .Where(s => s.StartDate <= info.Date)
                .Select(s => s.PartTimeFactor)
                .DefaultIfEmpty(1.0)
                .First();

            info.NominalWorkTime *= factor;

            return info;
        }
    }
}
