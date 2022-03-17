// -----------------------------------------------------------------------
// <copyright file="IUserMonthReportService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Reports.Domain.Model;

namespace RolXServer.Reports.Domain;

/// <summary>
/// Provides <see cref="UserMonthReport"/> instances.
/// </summary>
public interface IUserMonthReportService
{
    /// <summary>
    /// Gets the report for specified user and month.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="month">The month.</param>
    /// <returns>The report or <c>null</c> if the user doesn't exist.</returns>
    Task<UserMonthReport?> GetFor(Guid userId, DateTime month);
}
