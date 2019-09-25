// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace RolXServer.Auth
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
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddScoped<Domain.ISignInService, Domain.Detail.SignInService>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(Domain.Detail.BearerTokenFactory.Configure);

            return services;
        }
    }
}
