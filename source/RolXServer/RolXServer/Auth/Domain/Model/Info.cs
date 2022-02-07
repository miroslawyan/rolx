// -----------------------------------------------------------------------
// <copyright file="Info.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Auth.Domain.Model;

/// <summary>
/// The sign-in information.
/// </summary>
public sealed class Info
{
    /// <summary>
    /// Gets or sets the google client identifier.
    /// </summary>
    public string GoogleClientId { get; set; } = string.Empty;
}
