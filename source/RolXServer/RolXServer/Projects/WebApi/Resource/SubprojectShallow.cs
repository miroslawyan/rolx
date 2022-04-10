// -----------------------------------------------------------------------
// <copyright file="SubprojectShallow.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Projects.WebApi.Resource;

public record SubprojectShallow(
    int Id,
    string FullNumber,
    string CustomerName,
    string ProjectName,
    string Name,
    string ManagerName,
    bool IsClosed);
