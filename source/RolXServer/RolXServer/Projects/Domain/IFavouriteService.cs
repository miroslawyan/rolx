// -----------------------------------------------------------------------
// <copyright file="IFavouriteService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain;

/// <summary>
/// Provides access to favourite <see cref="Activity"/> instances.
/// </summary>
public interface IFavouriteService
{
    /// <summary>
    /// Gets all favourite activities of the specified user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>
    /// The favourite activities.
    /// </returns>
    Task<IEnumerable<Activity>> GetAll(Guid userId);

    /// <summary>
    /// Adds the specified activity to the favourites of the specified user.
    /// </summary>
    /// <param name="activity">The activity.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns>The async task.</returns>
    Task Add(Activity activity, Guid userId);

    /// <summary>
    /// Removes the specified activity from the favourites of the specified user.
    /// </summary>
    /// <param name="activity">The activity.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns>The async task.</returns>
    Task Remove(Activity activity, Guid userId);
}
