// -----------------------------------------------------------------------
// <copyright file="IPaidLeaveActivities.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain;

/// <summary>
/// Provides the (virtual) paid leave activities.
/// </summary>
public interface IPaidLeaveActivities
{
    /// <summary>
    /// Gets the subproject.
    /// </summary>
    Subproject Subproject { get; }

    /// <summary>
    /// Gets the <see cref="Activity" /> for the specified paid leave type.
    /// </summary>
    /// <param name="paidLeaveType">The paid leave type.</param>
    Activity this[PaidLeaveType paidLeaveType] { get; }

    /// <summary>
    /// Validates the project and subproject numbers of the specified subproject.
    /// </summary>
    /// <param name="subproject">The subproject.</param>
    void ValidateNumbers(Subproject subproject);
}
