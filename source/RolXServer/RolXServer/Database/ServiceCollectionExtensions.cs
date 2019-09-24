﻿// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using RolXServer.Common.DataAccess;

namespace RolXServer.Database
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/> instances.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the authentication services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Auth.DataAccess.Entity.User>, RolXRepository>();
        }
    }
}
