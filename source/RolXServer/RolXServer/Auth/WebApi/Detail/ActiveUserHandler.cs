// -----------------------------------------------------------------------
// <copyright file="ActiveUserHandler.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using RolXServer.Auth.Domain;
using RolXServer.Common.Util;

namespace RolXServer.Auth.WebApi.Detail
{
    /// <summary>
    /// Handles and checks the <see cref="ActiveUserRequirement"/>.
    /// </summary>
    internal sealed class ActiveUserHandler : AuthorizationHandler<ActiveUserRequirement>
    {
        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <returns>The async task.</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ActiveUserRequirement requirement)
        {
            if (context.User.IsActiveAt(DateTime.Today))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
