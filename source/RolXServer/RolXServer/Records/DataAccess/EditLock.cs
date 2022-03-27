// -----------------------------------------------------------------------
// <copyright file="EditLock.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace RolXServer.Records.DataAccess;

/// <summary>
/// The date before when the editing of records is locked.
/// </summary>
public sealed class EditLock
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the date.
    /// </summary>
    [Column(TypeName = "date")]
    public DateTime Date { get; set; }
}
