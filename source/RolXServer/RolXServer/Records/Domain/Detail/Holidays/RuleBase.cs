// -----------------------------------------------------------------------
// <copyright file="RuleBase.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Records.Domain.Detail.Holidays;

/// <summary>
/// Base class for holiday rules.
/// </summary>
public abstract class RuleBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RuleBase"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    protected RuleBase(string name)
    {
        this.Name = name;
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Determines whether the specified candidate is matching.
    /// </summary>
    /// <param name="candidate">The candidate.</param>
    /// <returns>
    ///   <c>true</c> if the specified candidate is matching; otherwise, <c>false</c>.
    /// </returns>
    public abstract bool IsMatching(DateOnly candidate);
}
