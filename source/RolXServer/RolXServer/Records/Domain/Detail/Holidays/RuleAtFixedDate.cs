// -----------------------------------------------------------------------
// <copyright file="RuleAtFixedDate.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace RolXServer.Records.Domain.Detail.Holidays
{
    /// <summary>
    /// A holiday rule matching fixed dates.
    /// </summary>
    public class RuleAtFixedDate : RuleBase
    {
        private readonly int month;
        private readonly int day;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleAtFixedDate" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        public RuleAtFixedDate(string name, int month, int day)
            : base(name)
        {
            this.month = month;
            this.day = day;
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
            return candidate.Month == this.month && candidate.Day == this.day;
        }
    }
}
