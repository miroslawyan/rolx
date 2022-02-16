// -----------------------------------------------------------------------
// <copyright file="ActivityExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain.Detail;

/// <summary>
/// Extension methods for <see cref="Activity"/> instances.
/// </summary>
internal static class ActivityExtensions
{
    private static readonly TimeSpan BudgetMin = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Sanitizes the specified activity.
    /// </summary>
    /// <param name="activity">The activity.</param>
    public static void Sanitize(this Activity activity)
    {
        activity.ResetFullName();
        activity.ClearEmptyBudget();
    }

    /// <summary>
    /// Sanitizes the specified activities.
    /// </summary>
    /// <param name="activities">The activities.</param>
    public static void Sanitize(this IEnumerable<Activity> activities)
    {
        foreach (var activity in activities)
        {
            activity.Sanitize();
        }
    }

    private static void ResetFullName(this Activity activity)
    {
        if (activity.Subproject is null)
        {
            throw new ArgumentException("Property 'Subproject' must be set", nameof(activity));
        }

        var subproject = activity.Subproject;
        activity.FullName = $"{subproject.Number}.{activity.Number:D3} - {subproject.Name} - {activity.Name}";
    }

    private static void ClearEmptyBudget(this Activity activity)
    {
        if ((activity.Budget ?? default) < BudgetMin)
        {
            activity.Budget = null;
        }
    }
}
