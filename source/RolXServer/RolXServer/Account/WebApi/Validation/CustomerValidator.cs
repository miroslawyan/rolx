// -----------------------------------------------------------------------
// <copyright file="CustomerValidator.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RolXServer.Account.DataAccess;
using RolXServer.Common.DataAccess;

namespace RolXServer.Account.WebApi.Validation
{
    /// <summary>
    /// Validator for <see cref="Customer"/> instances.
    /// </summary>
    public sealed class CustomerValidator : AbstractValidator<Customer>
    {
        private readonly IRepository<Customer> customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerValidator" /> class.
        /// </summary>
        /// <param name="customerRepository">The customer repository.</param>
        /// <param name="settingsAccessor">The settings accessor.</param>
        public CustomerValidator(
            IRepository<Customer> customerRepository,
            IOptions<Settings> settingsAccessor)
        {
            this.customerRepository = customerRepository;
            var settings = settingsAccessor.Value;

            this.RuleFor(c => c.Number)
                .NotNull()
                .NotEmpty().WithMessage("required")
                .Matches(settings.CustomerNumberPattern).WithMessage("pattern")
                .MustAsync(this.BeUnique);

            this.RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty();
        }

        private async Task<bool> BeUnique(Customer candidate, string newNumber, PropertyValidatorContext context, CancellationToken token)
        {
            if (await this.customerRepository.Entities
                .AnyAsync(c => c.Id != candidate.Id && c.Number == newNumber))
            {
                context.Rule.MessageBuilder = c => "notUnique";
                return false;
            }

            return true;
        }
    }
}
