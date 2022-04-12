// -----------------------------------------------------------------------
// <copyright file="PaidLeaveActivities.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain.Detail;

/// <summary>
/// Provides the (virtual) paid leave activities.
/// </summary>
public sealed class PaidLeaveActivities : IPaidLeaveActivities
{
    private readonly Dictionary<PaidLeaveType, Activity> activities;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaidLeaveActivities"/> class.
    /// </summary>
    public PaidLeaveActivities()
    {
        this.activities = Enum.GetValues<PaidLeaveType>()
            .ToDictionary(e => e, e => new Activity
            {
                Id = ((int)e) + 1,
                Number = ((int)e) + 1,
                Name = e.ToPrettyString(),
                StartDate = new DateOnly(2020, 1, 1),
                SubprojectId = this.Subproject.Id,
                Subproject = this.Subproject,
                BillabilityId = 7, // Abwesenheit. See RolXContext.SeedBillabilities
            });

        this.Subproject.Activities = this.activities.Values.ToList();
    }

    /// <inheritdoc/>
    public Subproject Subproject { get; } = new Subproject
    {
        Id = 1,
        ProjectNumber = 8900,
        Number = 1,
        CustomerName = "M&F",
        ProjectName = "Allgemein",
        Name = "Bezahlte Abwesenheiten",
    };

    /// <inheritdoc/>
    public Activity this[PaidLeaveType paidLeaveType]
        => this.activities[paidLeaveType];
}
