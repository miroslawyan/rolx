// -----------------------------------------------------------------------
// <copyright file="WorkItemGroup.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Immutable;

namespace RolXServer.Reports.Domain.Model;

public sealed record WorkItemGroup(
    string Name,
    IImmutableList<WorkItem> Items);
