// -----------------------------------------------------------------------
// <copyright file="ActivityValidator.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using RolXServer.Common.Util;
using RolXServer.Projects.WebApi.Resource;

namespace RolXServer.Projects.WebApi.Validation;

/// <summary>
/// Validator for <see cref="Activity"/> instances.
/// </summary>
public sealed class ActivityValidator : AbstractValidator<Activity>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActivityValidator"/> class.
    /// </summary>
    public ActivityValidator()
    {
        this.RuleFor(ph => ph.Number)
            .GreaterThan(0);

        this.RuleFor(ph => ph.Name)
            .NotNull()
            .NotEmpty();

        this.RuleFor(ph => ph.StartDate)
            .NotNull()
            .NotEmpty()
            .SetValidator(new IsoDateValidator<Activity>());

        this.RuleFor(ph => ph.EndDate)
            .NotEmpty()
            .SetValidator(new IsoDateValidator<Activity>())
            .GreaterThanOrEqualTo(ph => ph.StartDate)
            .When(ph => ph.EndDate is not null);

        this.RuleFor(ph => ph.Budget)
            .GreaterThanOrEqualTo(0);
    }
}
