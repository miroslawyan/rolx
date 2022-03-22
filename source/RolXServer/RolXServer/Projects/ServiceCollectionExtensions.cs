// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Projects;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/> instances.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the services of the Projects package.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>
    /// The service collection.
    /// </returns>
    public static IServiceCollection AddProjects(this IServiceCollection services)
    {
        return services.AddDomain();
    }

    private static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services
            .AddScoped<Domain.IFavouriteService, Domain.Detail.FavouriteService>()
            .AddScoped<Domain.IActivityService, Domain.Detail.ActivityService>()
            .AddScoped<Domain.ISubprojectService, Domain.Detail.SubprojectService>()
            .AddScoped<Domain.IPaidLeaveActivities, Domain.Detail.PaidLeaveActivities>();
    }
}
