// -----------------------------------------------------------------------
// <copyright file="IRecordService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RolXServer.WorkRecord.Domain.Model;

namespace RolXServer.WorkRecord.Domain
{
    /// <summary>
    /// Provides access to records.
    /// </summary>
    public interface IRecordService
    {
        /// <summary>
        /// Gets all records of the specified month, of the user with the specified identifier.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// The records.
        /// </returns>
        Task<IEnumerable<Record>> GetAllOfMonth(DateTime month, Guid userId);
    }
}
