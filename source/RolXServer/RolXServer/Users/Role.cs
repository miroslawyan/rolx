// -----------------------------------------------------------------------
// <copyright file="Role.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Users;

/// <summary>
/// The role of a user.
/// </summary>
public enum Role
{
    /// <summary>
    /// Just a user.
    /// </summary>
    User = 1,

    /// <summary>
    /// A user with some privileges.
    /// </summary>
    Supervisor = 2,

    /// <summary>
    /// A user with maximum privileges.
    /// </summary>
    Administrator = 1000,
}
