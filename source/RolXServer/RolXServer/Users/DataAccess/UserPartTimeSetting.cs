// -----------------------------------------------------------------------
// <copyright file="UserPartTimeSetting.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace RolXServer.Users.DataAccess;

/// <summary>
/// The part-time setting of a user.
/// </summary>
public sealed class UserPartTimeSetting
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the start date this setting is applicable.
    /// </summary>
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the new part-time factor.
    /// </summary>
    public double Factor { get; set; } = 1.0;
}
