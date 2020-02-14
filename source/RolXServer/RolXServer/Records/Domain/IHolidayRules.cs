// -----------------------------------------------------------------------
// <copyright file="IHolidayRules.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using RolXServer.Records.Domain.Model;

namespace RolXServer.Records.Domain
{
    /// <summary>
    /// Provides access to the holiday rules.
    /// </summary>
    public interface IHolidayRules
    {
        /// <summary>
        /// Applies the rules to the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>The record with the rules applied.</returns>
        Record Apply(Record record);
    }
}
