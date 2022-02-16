// -----------------------------------------------------------------------
// <copyright file="FavouriteActivity.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Users.DataAccess;

namespace RolXServer.Projects.DataAccess;

/// <summary>
/// A favourite activity of a user.
/// </summary>
public sealed class FavouriteActivity
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the user.
    /// </summary>
    public User? User { get; set; }

    /// <summary>
    /// Gets or sets the activity identifier.
    /// </summary>
    public int ActivityId { get; set; }

    /// <summary>
    /// Gets or sets the activity.
    /// </summary>
    public Activity? Activity { get; set; }
}
