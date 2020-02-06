// -----------------------------------------------------------------------
// <copyright file="ApplicationBuilderExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;

namespace RolXServer.Common.Errors
{
    /// <summary>
    /// Extension methods for <see cref="IApplicationBuilder"/> instances.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Uses the exception handler middleware.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The same builder.</returns>
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
