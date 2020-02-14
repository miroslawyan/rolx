// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RolXServer.Projects
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/> instances.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the services of the Projects package.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddAccount(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Settings>(configuration.GetSection("Projects"));

            return services.AddDomain();
        }

        private static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services
                .AddScoped<Domain.IFavouriteService, Domain.Detail.FavouriteService>()
                .AddScoped<Domain.IPhaseService, Domain.Detail.PhaseService>()
                .AddScoped<Domain.IProjectService, Domain.Detail.ProjectService>();
        }
    }
}
