// -----------------------------------------------------------------------
// <copyright file="SubprojectValidator.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using RolXServer.Projects.WebApi.Resource;

namespace RolXServer.Projects.WebApi.Validation;

/// <summary>
/// Validator for <see cref="Subproject"/> instances.
/// </summary>
public sealed class SubprojectValidator : AbstractValidator<Subproject>
{
    private readonly RolXContext dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SubprojectValidator" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="settingsAccessor">The settings accessor.</param>
    public SubprojectValidator(
        RolXContext dbContext,
        IOptions<Settings> settingsAccessor)
    {
        this.dbContext = dbContext;
        this.dbContext = dbContext;
        var settings = settingsAccessor.Value;

        this.RuleFor(p => p.Number)
            .NotNull()
            .NotEmpty().WithMessage("required")
            .Matches(settings.ProjectNumberPattern).WithMessage("pattern")
            .MustAsync(this.BeUnique);

        this.RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty().WithMessage("required");

        this.RuleFor(p => p.Activities)
            .Must(this.HaveUniqueNumbers)
            .MustAsync(this.BeOfCurrentSubproject);

        this.RuleForEach(p => p.Activities)
            .SetValidator(_ => new ActivityValidator());
    }

    private async Task<bool> BeUnique(Subproject candidate, string newNumber, ValidationContext<Subproject> context, CancellationToken token)
    {
        if (await this.dbContext.Subprojects
            .AnyAsync(p => p.Id != candidate.Id && p.Number == newNumber, token))
        {
            context.AddFailure("notUnique");
            return false;
        }

        return true;
    }

    private bool HaveUniqueNumbers(Subproject candidate, IEnumerable<Activity> activities, ValidationContext<Subproject> context)
    {
        if (activities.Select(ph => ph.Number)
            .GroupBy(n => n)
            .Any(g => g.Count() > 1))
        {
            context.AddFailure("activity numbers must be unique");
            return false;
        }

        return true;
    }

    private async Task<bool> BeOfCurrentSubproject(Subproject candidate, IEnumerable<Activity> activities, ValidationContext<Subproject> context, CancellationToken token)
    {
        var activityIds = activities.Select(ph => ph.Id)
            .Where(id => id != 0)
            .ToArray();

        if (await this.dbContext.Activities
            .Where(ph => activityIds.Contains(ph.Id))
            .AnyAsync(ph => ph.SubprojectId != candidate.Id))
        {
            context.AddFailure("activities must be of current subproject");
            return false;
        }

        return true;
    }
}
