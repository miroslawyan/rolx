// -----------------------------------------------------------------------
// <copyright file="IYearInfoService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Records.Domain.Model;

namespace RolXServer.Records.Domain;

/// <summary>
/// Provides access to yearInfos.
/// </summary>
public interface IYearInfoService
{
    /// <summary>
    /// Gets the holidays and monthly work times for the specified year.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <returns>The year info.</returns>
    YearInfo GetFor(int year);
}
