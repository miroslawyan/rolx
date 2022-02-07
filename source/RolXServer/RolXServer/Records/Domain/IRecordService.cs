// -----------------------------------------------------------------------
// <copyright file="IRecordService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Common.Util;
using RolXServer.Records.Domain.Model;

namespace RolXServer.Records.Domain;

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
    /// Updates the specified record.
    /// </summary>
    /// <param name="record">The record.</param>
    /// <returns>The async task.</returns>
    Task Update(Record record);
}
