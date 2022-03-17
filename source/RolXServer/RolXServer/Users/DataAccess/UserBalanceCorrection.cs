// -----------------------------------------------------------------------
// <copyright file="UserBalanceCorrection.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace RolXServer.Users.DataAccess;

/// <summary>
/// A correction on the balance of a user.
/// </summary>
public sealed class UserBalanceCorrection
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
    /// Gets or sets the date of this correction.
    /// </summary>
    [Column(TypeName = "date")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the overtime correction in seconds.
    /// </summary>
    public long OvertimeSeconds { get; set; }

    /// <summary>
    /// Gets or sets the overtime correction.
    /// </summary>
    [NotMapped]
    public TimeSpan Overtime
    {
        get => TimeSpan.FromSeconds(this.OvertimeSeconds);
        set => this.OvertimeSeconds = (long)value.TotalSeconds;
    }

    /// <summary>
    /// Gets or sets the vacation correction in seconds.
    /// </summary>
    public long VacationSeconds { get; set; }

    /// <summary>
    /// Gets or sets the vacation correction.
    /// </summary>
    [NotMapped]
    public TimeSpan Vacation
    {
        get => TimeSpan.FromSeconds(this.VacationSeconds);
        set => this.VacationSeconds = (long)value.TotalSeconds;
    }
}
