// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RolXServer.Records
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/> instances.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the services of the Records package.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddWorkRecord(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Settings>(configuration.GetSection("Records"));

            services.AddScoped<Domain.IBalanceService, Domain.Detail.BalanceService>();
            services.AddSingleton<Domain.IDayInfoService, Domain.Detail.DayInfoService>();
            services.AddScoped<Domain.IRecordService, Domain.Detail.RecordService>();
            services.AddSingleton<Domain.IHolidayRules, Domain.Detail.StaticHolidayRules>();

            return services;
        }
    }
}
