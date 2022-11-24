// -----------------------------------------------------------------------
// <copyright file="Activity.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Projects.WebApi.Resource;

/// <summary>
/// An activity in a subproject.
/// </summary>
public sealed record Activity(
    int Id,
    int Number,
    string Name,
    string StartDate,
    string? EndDate,
    int BillabilityId,
    string BillabilityName,
    bool IsBillable,
    long Budget,
    long Planned,
    long Actual,
    string ProjectName,
    string SubprojectName,
    string CustomerName,
    string FullNumber,
    string FullName,
    string AllSubprojectNames);
