// -----------------------------------------------------------------------
// <copyright file="SubprojectValidator.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;

using RolXServer.Projects.WebApi.Resource;

namespace RolXServer.Projects.WebApi.Validation;

/// <summary>
/// Validator for <see cref="Subproject"/> instances.
/// </summary>
public sealed class SubprojectValidator : AbstractValidator<Subproject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubprojectValidator" /> class.
    /// </summary>
    public SubprojectValidator()
    {
        this.RuleFor(s => s.Number)
            .GreaterThan(0)
            .LessThan(1000);

        this.RuleFor(s => s.Name)
            .NotNull()
            .NotEmpty().WithMessage("required");

        this.RuleFor(s => s.ProjectNumber)
            .GreaterThan(0)
            .LessThan(10000);

        this.RuleFor(s => s.ProjectName)
            .NotNull()
            .NotEmpty().WithMessage("required");

        this.RuleFor(s => s.CustomerName)
            .NotNull()
            .NotEmpty().WithMessage("required");

        this.RuleFor(s => s.Activities)
            .Must(this.HaveUniqueNumbers);

        this.RuleForEach(s => s.Activities)
            .SetValidator(_ => new ActivityValidator());
    }

    private bool HaveUniqueNumbers(Subproject candidate, IEnumerable<Activity> activities, ValidationContext<Subproject> context)
    {
        if (activities.Select(a => a.Number)
            .GroupBy(n => n)
            .Any(g => g.Count() > 1))
        {
            context.AddFailure("activity numbers must be unique within the subproject");
            return false;
        }

        return true;
    }
}
