// -----------------------------------------------------------------------
// <copyright file="RuleEasterBased.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace RolXServer.Records.Domain.Detail.Holiday
{
    /// <summary>
    /// Rule for holidays in relation to the easter Sunday of the requested year.
    /// </summary>
    public sealed class RuleEasterBased : RuleBase
    {
        private readonly int offset;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEasterBased"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="offset">The offset.</param>
        public RuleEasterBased(string name, int offset)
            : base(name)
        {
            this.offset = offset;
        }

        /// <summary>
        /// Determines whether the specified candidate is matching.
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns>
        ///   <c>true</c> if the specified candidate is matching; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsMatching(DateTime candidate)
        {
            var holiday = CalculateWesternEasterSunday(candidate.Year).AddDays(this.offset);
            return candidate.Date == holiday;
        }

        private static DateTime CalculateWesternEasterSunday(int year)
        {
            var a = year % 19;
            var b = year / 100;
            var c = year % 100;
            var d = ((19 * a) + b - (b / 4) - ((b - ((b + 8) / 25) + 1) / 3) + 15) % 30;
            var e = (32 + (2 * (b % 4)) + (2 * (c / 4)) - d - (c % 4)) % 7;
            var f = d + e - (7 * ((a + (11 * d) + (22 * e)) / 451)) + 114;
            var month = f / 31;
            var day = (f % 31) + 1;
            return new DateTime(year, month, day);
        }
    }
}
