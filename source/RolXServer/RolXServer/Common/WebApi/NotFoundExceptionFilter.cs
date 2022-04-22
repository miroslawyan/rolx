// -----------------------------------------------------------------------
// <copyright file="NotFoundExceptionFilter.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using NLog;

namespace RolXServer.Common.WebApi;

/// <summary>
/// Handles <see cref="NotFoundException"/> and translates them to 404 responses.
/// </summary>
public class NotFoundExceptionFilter : IActionFilter, IOrderedFilter
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    /// <inheritdoc/>
    public int Order { get; } = int.MaxValue - 10;

    /// <inheritdoc/>
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    /// <inheritdoc/>
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null && context.Exception is NotFoundException)
        {
            logger.Warn(context.Exception, "Error in HTTP Request to {}", context.HttpContext.Request);

            var statusCode = (int)HttpStatusCode.NotFound;
            context.Result = new ObjectResult(new Dictionary<string, object>
            {
                { "title", context.Exception.Message },
                { "status", statusCode },
            })
            {
                StatusCode = statusCode,
            };

            context.ExceptionHandled = true;
        }
    }
}
