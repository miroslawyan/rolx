// -----------------------------------------------------------------------
// <copyright file="Rules.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace RolXServer.WorkRecord.Domain.Holiday
{
    /// <summary>
    /// Provides the holiday rules.
    /// </summary>
    public static class Rules
    {
        /// <summary>
        /// All rules.
        /// </summary>
        public static readonly IEnumerable<HolidayRuleBase> All = CreateAll().ToList();

        private static IEnumerable<HolidayRuleBase> CreateAll()
        {
            yield return new HolidayRuleAtFixedDate("Neujahr", 1, 1);
            yield return new HolidayRuleAtFixedDate("Berchtoldstag", 1, 2);
            yield return new HolidayRuleAtFixedDate("Tag der Arbeit", 5, 1);
            yield return new HolidayRuleAtFixedDate("Nationalfeiertag", 8, 1);
            yield return new HolidayRuleAtFixedDate("Weihnachten", 12, 25);
            yield return new HolidayRuleAtFixedDate("Stephanstag", 12, 25);

            yield return new HolidayRuleEasterBased("Karfreitag", -2);
            yield return new HolidayRuleEasterBased("Ostern", 0);
            yield return new HolidayRuleEasterBased("Ostermontag", 1);
            yield return new HolidayRuleEasterBased("Christi Himmelfahrt", 39);
            yield return new HolidayRuleEasterBased("Pfingsten", 49);
            yield return new HolidayRuleEasterBased("Pfingstmontag", 50);
        }
    }
}
