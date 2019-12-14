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

using RolXServer.Common.Util;
using RolXServer.WorkRecord.Domain.Model;

namespace RolXServer.WorkRecord.Domain
{
    /// <summary>
    /// Provides access to records.
    /// </summary>
    public interface IRecordService
    {
        /// <summary>
        /// Gets all records of the specified range, of the user with the specified identifier.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// The requested records.
        /// </returns>
        Task<IEnumerable<Record>> GetRange(DateRange range, Guid userId);

        /// <summary>
        /// Creates the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>The created record.</returns>
        Task<Record> Create(Record record);

        /// <summary>
        /// Updates the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>The async task.</returns>
        Task Update(Record record);
    }
}
