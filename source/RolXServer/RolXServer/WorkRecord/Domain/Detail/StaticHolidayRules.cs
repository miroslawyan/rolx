// -----------------------------------------------------------------------
// <copyright file="StaticHolidayRules.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using RolXServer.WorkRecord.Domain.Detail.Holiday;
using RolXServer.WorkRecord.Domain.Model;

namespace RolXServer.WorkRecord.Domain.Detail
{
    /// <summary>
    /// The static holiday rules.
    /// </summary>
    public class StaticHolidayRules : IHolidayRules
    {
        private readonly IEnumerable<RuleBase> all = CreateAll().ToList();

        /// <summary>
        /// Applies the rules to the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>
        /// The record with the rules applied.
        /// </returns>
        public Record Apply(Record record)
        {
            var rule = this.all.FirstOrDefault(r => r.IsMatching(record.Date));
            if (rule != null)
            {
                record.Name = rule.Name;
            }

            return record;
        }

        private static IEnumerable<RuleBase> CreateAll()
        {
            yield return new RuleAtFixedDate("Neujahr", 1, 1);
            yield return new RuleAtFixedDate("Berchtoldstag", 1, 2);
            yield return new RuleAtFixedDate("Tag der Arbeit", 5, 1);
            yield return new RuleAtFixedDate("Nationalfeiertag", 8, 1);
            yield return new RuleAtFixedDate("Weihnachten", 12, 25);
            yield return new RuleAtFixedDate("Stephanstag", 12, 25);

            yield return new RuleEasterBased("Karfreitag", -2);
            yield return new RuleEasterBased("Ostern", 0);
            yield return new RuleEasterBased("Ostermontag", 1);
            yield return new RuleEasterBased("Christi Himmelfahrt", 39);
            yield return new RuleEasterBased("Pfingsten", 49);
            yield return new RuleEasterBased("Pfingstmontag", 50);
        }
    }
}
