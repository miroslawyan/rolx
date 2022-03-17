// -----------------------------------------------------------------------
// <copyright file="ClaimsPrincipalExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Claims;

using RolXServer.Common.Util;
using RolXServer.Users;

namespace RolXServer.Auth.Domain;

/// <summary>
/// Extension methods for <see cref="ClaimsPrincipal"/> instances.
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Gets the user identifier of the specified principal.
    /// </summary>
    /// <param name="principal">The principal.</param>
    /// <returns>The user identifier.</returns>
    public static Guid GetUserId(this ClaimsPrincipal principal)
        => Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);

    /// <summary>
    /// Gets the user identifier of the specified principal.
    /// </summary>
    /// <param name="principal">The principal.</param>
    /// <returns>The user identifier.</returns>
    public static Role GetRole(this ClaimsPrincipal principal)
        => Enum.Parse<Role>(principal.FindFirstValue(ClaimTypes.Role) ?? string.Empty);

    /// <summary>
    /// Gets the entry date.
    /// </summary>
    /// <param name="principal">The principal.</param>
    /// <returns>The entry date.</returns>
    public static DateTime? GetEntryDate(this ClaimsPrincipal principal)
        => IsoDate.ParseNullable(principal.FindFirstValue(RolXClaimTypes.EntryDate));

    /// <summary>
    /// Gets the left date.
    /// </summary>
    /// <param name="principal">The principal.</param>
    /// <returns>The left date.</returns>
    public static DateTime? GetLeftDate(this ClaimsPrincipal principal)
        => IsoDate.ParseNullable(principal.FindFirstValue(RolXClaimTypes.LeftDate));

    /// <summary>
    /// Determines whether the specified principal is active at the specified date.
    /// </summary>
    /// <param name="principal">The principal.</param>
    /// <param name="date">The date.</param>
    /// <returns>
    ///   <c>true</c> if specified principal is active at the specified date; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsActiveAt(this ClaimsPrincipal principal, DateTime date)
    {
        try
        {
            var entryDate = principal.GetEntryDate();
            var leftDate = principal.GetLeftDate();

            return entryDate.HasValue
                && entryDate.Value <= date
                && (!leftDate.HasValue || leftDate.Value > date);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
