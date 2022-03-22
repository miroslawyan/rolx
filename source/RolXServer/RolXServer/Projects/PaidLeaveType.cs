// -----------------------------------------------------------------------
// <copyright file="PaidLeaveType.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Projects;

/// <summary>
/// The types of paid leaves.
/// </summary>
public enum PaidLeaveType
{
    /// <summary>
    /// Vacation, holidays, vacay or whatever you like to call this nice but by far too short time of not working.
    /// </summary>
    Vacation,

    /// <summary>
    /// Sickness or any other medical reason of not being able to work.
    /// </summary>
    Sickness,

    /// <summary>
    /// Military-, community-service or civil defense.
    /// </summary>
    MilitaryService,

    /// <summary>
    /// Other reasons for legally taking a day off, like marriage, becoming parent, moving or a death in the family.
    /// Requires a comment stating the exact reason.
    /// </summary>
    Other,
}
