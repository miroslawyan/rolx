// -----------------------------------------------------------------------
// <copyright file="ProjectValidator.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RolXServer.Account.WebApi.Resource;
using RolXServer.Common.DataAccess;

namespace RolXServer.Account.WebApi.Validation
{
    /// <summary>
    /// Validator for <see cref="Project"/> instances.
    /// </summary>
    public sealed class ProjectValidator : AbstractValidator<Project>
    {
        private readonly IRepository<DataAccess.Project> projectRepository;
        private readonly IRepository<DataAccess.Customer> customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectValidator" /> class.
        /// </summary>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="customerRepository">The customer repository.</param>
        /// <param name="settingsAccessor">The settings accessor.</param>
        public ProjectValidator(
            IRepository<DataAccess.Project> projectRepository,
            IRepository<DataAccess.Customer> customerRepository,
            IOptions<Settings> settingsAccessor)
        {
            this.projectRepository = projectRepository;
            this.customerRepository = customerRepository;
            var settings = settingsAccessor.Value;

            this.RuleFor(p => p.Number)
                .NotNull()
                .NotEmpty().WithMessage("required")
                .Matches(settings.ProjectNumberPattern).WithMessage("pattern")
                .MustAsync(this.BeUnique);

            this.RuleFor(p => p.Customer)
                .NotNull().WithMessage("required")
                .MustAsync(this.Exist);

            this.RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty().WithMessage("required");
        }

        private async Task<bool> BeUnique(Project candidate, string newNumber, PropertyValidatorContext context, CancellationToken token)
        {
            if (await this.projectRepository.Entities
                .AnyAsync(p => p.Id != candidate.Id && p.Number == newNumber, token))
            {
                context.Rule.MessageBuilder = c => "notUnique";
                return false;
            }

            return true;
        }

        private async Task<bool> Exist(Project candidate, DataAccess.Customer? newCustomer, PropertyValidatorContext context, CancellationToken token)
        {
            Debug.Assert(newCustomer != null, "We have a rule for this.");

            if (await this.customerRepository.Entities.AnyAsync(c => c.Id == newCustomer.Id, token))
            {
                return true;
            }

            context.Rule.MessageBuilder = c => "notExisting";
            return false;
        }
    }
}
