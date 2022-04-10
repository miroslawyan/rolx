// -----------------------------------------------------------------------
// <copyright file="Subproject.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Projects.WebApi.Resource;

/// <summary>
/// A subproject we are working on.
/// </summary>
public sealed record Subproject(
    int Id,
    string FullNumber,
    string FullName,
    string CustomerName,
    int ProjectNumber,
    string ProjectName,
    int Number,
    string Name,
    Guid? ManagerId,
    string ManagerName,
    bool IsClosed,
    ImmutableList<Activity> Activities)
    : SubprojectShallow(
        Id: Id,
        FullNumber: FullNumber,
        CustomerName: CustomerName,
        ProjectName: ProjectName,
        Name: Name,
        ManagerName: ManagerName,
        IsClosed: IsClosed);
