// -----------------------------------------------------------------------
// <copyright file="ProjectValidator.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RolXServer.Account.WebApi.Resource;

namespace RolXServer.Account.WebApi.Validation
{
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
        }

        private async Task<bool> BeUnique(Project candidate, string newNumber, PropertyValidatorContext context, CancellationToken token)
        {
            if (await this.dbContext.Projects
                .AnyAsync(p => p.Id != candidate.Id && p.Number == newNumber, token))
            {
                context.Rule.MessageBuilder = c => "notUnique";
                return false;
            }

            return true;
        }

        private bool HaveUniqueNumbers(Project candidate, IEnumerable<Phase> phases, PropertyValidatorContext context)
        {
            if (phases.Select(ph => ph.Number)
                .GroupBy(n => n)
                .Any(g => g.Count() > 1))
            {
                context.Rule.MessageBuilder = c => "phase numbers must be unique";
                return false;
            }

            return true;
        }

        private async Task<bool> BeOfCurrentProject(Project candidate, IEnumerable<Phase> phases, PropertyValidatorContext context, CancellationToken token)
        {
            var phaseIds = phases.Select(ph => ph.Id)
                .Where(id => id != 0)
                .ToArray();

            if (await this.dbContext.Phases
                .Where(ph => phaseIds.Contains(ph.Id))
                .AnyAsync(ph => ph.ProjectId != candidate.Id))
            {
                context.Rule.MessageBuilder = c => "phases must be of current project";
                return false;
            }

            return true;
        }
    }
}
