// -----------------------------------------------------------------------
// <copyright file="ProjectValidator.cs" company="Christian Ewald">
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
/// Validator for <see cref="Project"/> instances.
/// </summary>
public sealed class ProjectValidator : AbstractValidator<Project>
{
    private readonly RolXContext dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectValidator" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="settingsAccessor">The settings accessor.</param>
    public ProjectValidator(
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

        this.RuleFor(p => p.Phases)
            .Must(this.HaveUniqueNumbers)
            .MustAsync(this.BeOfCurrentProject);

        this.RuleForEach(p => p.Phases)
            .SetValidator(_ => new PhaseValidator());
    }

    private async Task<bool> BeUnique(Project candidate, string newNumber, ValidationContext<Project> context, CancellationToken token)
    {
        if (await this.dbContext.Projects
            .AnyAsync(p => p.Id != candidate.Id && p.Number == newNumber, token))
        {
            context.AddFailure("notUnique");
            return false;
        }

        return true;
    }

    private bool HaveUniqueNumbers(Project candidate, IEnumerable<Phase> phases, ValidationContext<Project> context)
    {
        if (phases.Select(ph => ph.Number)
            .GroupBy(n => n)
            .Any(g => g.Count() > 1))
        {
            context.AddFailure("phase numbers must be unique");
            return false;
        }

        return true;
    }

    private async Task<bool> BeOfCurrentProject(Project candidate, IEnumerable<Phase> phases, ValidationContext<Project> context, CancellationToken token)
    {
        var phaseIds = phases.Select(ph => ph.Id)
            .Where(id => id != 0)
            .ToArray();

        if (await this.dbContext.Phases
            .Where(ph => phaseIds.Contains(ph.Id))
            .AnyAsync(ph => ph.ProjectId != candidate.Id))
        {
            context.AddFailure("phases must be of current project");
            return false;
        }

        return true;
    }
}
