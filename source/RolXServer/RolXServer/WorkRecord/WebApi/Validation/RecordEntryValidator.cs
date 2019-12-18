// -----------------------------------------------------------------------
// <copyright file="RecordEntryValidator.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Validators;
using RolXServer.Common.Util;
using RolXServer.WorkRecord.WebApi.Resource;

namespace RolXServer.WorkRecord.WebApi.Validation
{
    /// <summary>
    /// Validator for <see cref="RecordEntry"/> instances.
    /// </summary>
    internal sealed class RecordEntryValidator : AbstractValidator<RecordEntry>
    {
        private readonly Record parent;
        private readonly RolXContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordEntryValidator" /> class.
        /// </summary>
        /// <param name="parent">The parent record.</param>
        /// <param name="dbContext">The database context.</param>
        public RecordEntryValidator(Record parent, RolXContext dbContext)
        {
            this.parent = parent;
            this.dbContext = dbContext;

            this.RuleFor(e => e.PhaseId)
                .NotEqual(0);

            this.RuleFor(e => e.PhaseId)
                .MustAsync(this.BeOfExistingAndOpenPhase)
                .Unless(e => e.Duration == 0);

            this.RuleFor(e => e.Duration)
                .GreaterThanOrEqualTo(0);
        }

        private async Task<bool> BeOfExistingAndOpenPhase(RecordEntry candidate, int phaseId, PropertyValidatorContext context, CancellationToken token)
        {
            var phase = await this.dbContext.Phases.FindAsync(phaseId);
            if (await this.dbContext.Phases.FindAsync(phaseId) == null)
            {
                context.Rule.MessageBuilder = c => "phaseId must be of an existing phase";
                return false;
            }

            var recordDate = IsoDate.Parse(this.parent.Date);
            if (phase.StartDate > recordDate)
            {
                context.Rule.MessageBuilder = c => "phase isn't open yet";
                return false;
            }

            if (phase.EndDate.HasValue && phase.EndDate.Value < recordDate)
            {
                context.Rule.MessageBuilder = c => "phase has already been closed";
                return false;
            }

            return true;
        }
    }
}
