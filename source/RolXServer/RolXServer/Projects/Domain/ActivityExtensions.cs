// -----------------------------------------------------------------------
// <copyright file="ActivityExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain;

/// <summary>
/// Extension methods for <see cref="Activity"/> instances.
/// </summary>
internal static class ActivityExtensions
{
    private static readonly TimeSpan BudgetMin = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Gets the full-qualified number of the specified activity.
    /// </summary>
    /// <param name="activity">The activity.</param>
    /// <returns>The full-qualified number.</returns>
    public static string FullNumber(this Activity activity)
    {
        if (activity.Subproject is null)
        {
            throw new ArgumentNullException("The activities subproject must not be null", nameof(activity));
        }

        return $"{activity.Subproject.FullNumber()}.{activity.Number:D2}";
    }

    /// <summary>
    /// Gets the full-qualified name of the specified activity.
    /// </summary>
    /// <param name="activity">The activity.</param>
    /// <returns>The full-qualified name.</returns>
    public static string FullName(this Activity activity)
    {
        if (activity.Subproject is null)
        {
            throw new ArgumentNullException("The activities subproject must not be null", nameof(activity));
        }

        return $"{activity.Subproject.AllNames()} - {activity.Name} ({activity.FullNumber()})";
    }

    /// <summary>
    /// Gets the numbered name of the specified activity.
    /// </summary>
    /// <param name="activity">The activity.</param>
    /// <returns>The numbered name.</returns>
    public static string NumberedName(this Activity activity)
        => $"{activity.Name} ({activity.Number:D2})";

    /// <summary>
    /// Sanitizes the specified activity.
    /// </summary>
    /// <param name="activity">The activity.</param>
    internal static void Sanitize(this Activity activity)
    {
        activity.ClearEmptyBudget();
        activity.ClearBillabilityReference();
    }

    /// <summary>
    /// Sanitizes the specified activities.
    /// </summary>
    /// <param name="activities">The activities.</param>
    internal static void Sanitize(this IEnumerable<Activity> activities)
    {
        foreach (var activity in activities)
        {
            activity.Sanitize();
        }
    }

    private static void ClearEmptyBudget(this Activity activity)
    {
        if ((activity.Budget ?? default) < BudgetMin)
        {
            activity.Budget = null;
        }
    }

    private static void ClearBillabilityReference(this Activity activity)
        => activity.Billability = null;
}
