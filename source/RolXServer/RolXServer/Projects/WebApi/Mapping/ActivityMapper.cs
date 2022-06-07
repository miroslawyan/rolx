// -----------------------------------------------------------------------
// <copyright file="ActivityMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Common.Util;
using RolXServer.Projects.Domain;

namespace RolXServer.Projects.WebApi.Mapping;

/// <summary>
/// Maps activities from / to resource.
/// </summary>
internal static class ActivityMapper
{
    /// <summary>
    /// Converts to resource.
    /// </summary>
    /// <param name="domain">The domain.</param>
    /// <param name="actualSums">The actual sums.</param>
    /// <returns>The resource.</returns>
    public static Resource.Activity ToResource(this DataAccess.Activity domain, IDictionary<int, TimeSpan>? actualSums = null)
        => new Resource.Activity(
            Id: domain.Id,
            Number: domain.Number,
            Name: domain.Name,
            StartDate: domain.StartDate.ToIsoDate(),
            EndDate: domain.EndedDate?.AddDays(-1).ToIsoDate(),
            BillabilityId: domain.Billability!.Id,
            BillabilityName: domain.Billability!.Name,
            IsBillable: domain.Billability!.IsBillable,
            Budget: (long)(domain.Budget?.TotalSeconds ?? 0),
            Actual: GetActualSumSeconds(domain.Id, actualSums),
            ProjectName: domain.Subproject?.ProjectName ?? string.Empty,
            SubprojectName: domain.Subproject?.Name ?? string.Empty,
            CustomerName: domain.Subproject?.CustomerName ?? string.Empty,
            FullNumber: domain.FullNumber(),
            FullName: domain.FullName(),
            AllSubprojectNames: domain.Subproject!.AllNames());

    /// <summary>
    /// Converts to domain.
    /// </summary>
    /// <param name="resource">The resource.</param>
    /// <param name="subproject">The subproject.</param>
    /// <returns>
    /// The domain.
    /// </returns>
    public static DataAccess.Activity ToDomain(this Resource.Activity resource, DataAccess.Subproject? subproject = null) =>
        new DataAccess.Activity
        {
            Id = resource.Id,
            Number = resource.Number,
            SubprojectId = subproject?.Id ?? 0,
            Subproject = subproject,
            Name = resource.Name,
            StartDate = IsoDate.Parse(resource.StartDate),
            EndedDate = IsoDate.ParseNullable(resource.EndDate)?.AddDays(1),
            Budget = TimeSpan.FromSeconds(resource.Budget),
            BillabilityId = resource.BillabilityId,
        };

    private static long GetActualSumSeconds(int activityId, IDictionary<int, TimeSpan>? actualSums)
    {
        if (actualSums?.TryGetValue(activityId, out var actualSum) ?? false)
        {
            return (long)actualSum.TotalSeconds;
        }

        return 0;
    }
}
