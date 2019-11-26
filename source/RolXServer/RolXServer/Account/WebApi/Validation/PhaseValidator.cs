// -----------------------------------------------------------------------
// <copyright file="PhaseValidator.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using RolXServer.Account.WebApi.Resource;
using RolXServer.Common.Util;

namespace RolXServer.Account.WebApi.Validation
{
    /// <summary>
    /// Validator for <see cref="Phase"/> instances.
    /// </summary>
    public sealed class PhaseValidator : AbstractValidator<Phase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhaseValidator"/> class.
        /// </summary>
        public PhaseValidator()
        {
            this.RuleFor(ph => ph.Number)
                .GreaterThan(0);

            this.RuleFor(ph => ph.Name)
                .NotNull()
                .NotEmpty();

            this.RuleFor(ph => ph.StartDate)
                .NotNull()
                .NotEmpty()
                .SetValidator(new IsoDateValidator());

            this.RuleFor(ph => ph.EndDate)
                .NotEmpty()
                .SetValidator(new IsoDateValidator())
                .GreaterThanOrEqualTo(ph => ph.StartDate)
                .Unless(ph => ph.EndDate == null);

            this.RuleFor(ph => ph.BudgetHours)
                .GreaterThanOrEqualTo(0)
                .Unless(ph => !ph.BudgetHours.HasValue);
        }
    }
}
