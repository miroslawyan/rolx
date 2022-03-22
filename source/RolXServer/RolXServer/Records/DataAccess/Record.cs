// -----------------------------------------------------------------------
// <copyright file="Record.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

using RolXServer.Projects;
using RolXServer.Users.DataAccess;

namespace RolXServer.Records.DataAccess;

/// <summary>
/// A record for a day.
/// </summary>
public sealed class Record
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

    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the user.
    /// </summary>
    public User? User { get; set; }

    /// <summary>
    /// Gets or sets the type of paid leave.
    /// </summary>
    public PaidLeaveType? PaidLeaveType { get; set; }

    /// <summary>
    /// Gets or sets the paid leave reason.
    /// </summary>
    public string? PaidLeaveReason { get; set; }

    /// <summary>
    /// Gets or sets the entries.
    /// </summary>
    public List<RecordEntry> Entries { get; set; } = new List<RecordEntry>();
}
