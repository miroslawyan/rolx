// -----------------------------------------------------------------------
// <copyright file="WorkItemGroupExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;

using RolXServer.Common.Util;
using RolXServer.Reports.Domain.Model;

namespace RolXServer.Reports.Domain.Detail;

/// <summary>
/// Extension methods for <see cref="WorkItemGroup"/> instances.
/// </summary>
internal static class WorkItemGroupExtensions
{
    /// <summary>
    /// Merges the specified additional group into the specified group.
    /// </summary>
    /// <param name="groups">The groups.</param>
    /// <param name="additionalGroup">The additional group.</param>
    /// <returns>The merged groups.</returns>
    public static IEnumerable<WorkItemGroup> Merge(this IEnumerable<WorkItemGroup> groups, WorkItemGroup additionalGroup)
    {
        var hasMerged = false;

        foreach (var group in groups)
        {
            if (group.Name == additionalGroup.Name)
            {
                hasMerged = true;
                yield return group.Merge(additionalGroup);
            }
            else
            {
                yield return group;
            }
        }

        if (!hasMerged)
        {
            yield return additionalGroup;
        }
    }

    private static WorkItemGroup Merge(this WorkItemGroup thisGroup, WorkItemGroup otherGroup)
    {
        Debug.Assert(thisGroup.Name == otherGroup.Name, "Group names must match");

        var items = thisGroup.Items.Concat(otherGroup.Items)
            .GroupBy(item => item.Name)
            .Select(group => new WorkItem(group.Key, group.Select(item => item.Duration).Sum()));

        return new WorkItemGroup(thisGroup.Name, items.ToImmutableList());
    }
}
