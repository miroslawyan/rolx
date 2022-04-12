// -----------------------------------------------------------------------
// <copyright file="IActivityService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain;

/// <summary>
/// Provides access to <see cref="Activity"/> instances.
/// </summary>
public interface IActivityService
{
    /// <summary>
    /// Gets all activities not closed before the specified date.
    /// </summary>
    /// <param name="unlessEndedBefore">The unless ended before date.</param>
    /// <returns>
    /// The activities.
    /// </returns>
    Task<IEnumerable<Activity>> GetAll(DateOnly? unlessEndedBefore);

    /// <summary>
    /// Gets the suitable activities for the specified user at the specified date.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="date">The date.</param>
    /// <returns>
    /// The suitable activities.
    /// </returns>
    Task<IEnumerable<Activity>> GetSuitable(Guid userId, DateOnly date);

    /// <summary>
    /// Gets the sum of actual times spent on the activities of the specified subproject.
    /// </summary>
    /// <param name="subprojectId">The subproject identifier.</param>
    /// <returns>A dictionary from activity identifiers to the sum of actual times spent.</returns>
    Task<IDictionary<int, TimeSpan>> GetActualSums(int subprojectId);
}
