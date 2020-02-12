// -----------------------------------------------------------------------
// <copyright file="ActiveUserRequirement.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;

namespace RolXServer.Auth.WebApi.Detail
{
    /// <summary>
    /// Requires a user to be active, which means, her entry date is in the past and her leaving date in the future.
    /// </summary>
    /// <remarks>
    /// Users are active, if their entry date is in the past and their leaving date in the future.
    /// </remarks>
    internal sealed class ActiveUserRequirement : IAuthorizationRequirement
    {
    }
}
