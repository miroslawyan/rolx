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
internal sealed class PaidLeaveActivities : IPaidLeaveActivities
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
                Id = -1,
                Number = ((int)e) + 1,
                Name = e.ToPrettyString(),
                Subproject = this.Subproject,
            });

        this.Subproject.Activities = this.activities.Values.ToList();
    }

    /// <inheritdoc/>
    public Subproject Subproject { get; } = new Subproject
    {
        Id = -1,
        ProjectNumber = 8990,
        Number = 1,
        CustomerName = "M&F",
        ProjectName = "Intern",
        Name = "Abwesenheiten",
    };

    /// <inheritdoc/>
    public Activity this[PaidLeaveType paidLeaveType]
        => this.activities[paidLeaveType];

    /// <inheritdoc/>
    public void ValidateNumbers(Subproject subproject)
    {
        if (this.Subproject.ProjectNumber == subproject.ProjectNumber
            && this.Subproject.Number == subproject.Number)
        {
            throw new InvalidOperationException("Sub-/project numbers are already in use by the paid leaves subproject");
        }
    }
}
