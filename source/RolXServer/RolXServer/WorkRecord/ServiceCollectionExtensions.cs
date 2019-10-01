// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

namespace RolXServer.WorkRecord
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/> instances.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the services of the WorkRecord package.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddWorkRecord(this IServiceCollection services)
        {
            services.AddSingleton<Domain.IHolidayRules, Domain.Detail.StaticHolidayRules>();
            services.AddScoped<Domain.IRecordService, Domain.Detail.RecordService>();

            return services;
        }
    }
}
