// -----------------------------------------------------------------------
// <copyright file="RecordEntryValidator.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;

using RolXServer.Common.Util;
using RolXServer.Records.WebApi.Resource;

namespace RolXServer.Records.WebApi.Validation;

/// <summary>
/// Validator for <see cref="RecordEntry"/> instances.
/// </summary>
internal sealed class RecordEntryValidator : AbstractValidator<RecordEntry>
{
    private const int SecondsPerDay = 24 * 3600;

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

        this.RuleFor(e => e.Begin)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(SecondsPerDay);

        this.RuleFor(e => e.Pause)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(SecondsPerDay);

        this.RuleFor(e => e.Pause)
            .Null()
            .Unless(e => e.Begin.HasValue);

        this.RuleFor(e => e.Duration)
            .Must(EndWhithinSameDay)
            .Unless(e => !e.Begin.HasValue);
    }

    private static bool EndWhithinSameDay(RecordEntry candidate, long duration, ValidationContext<RecordEntry> context)
    {
        var end = duration + (candidate.Begin ?? 0) + (candidate.Pause ?? 0);
        if (end > SecondsPerDay)
        {
            context.AddFailure("begin + pause + duration must be within 24h");
            return false;
        }

        return true;
    }

    private async Task<bool> BeOfExistingAndOpenPhase(RecordEntry candidate, int phaseId, ValidationContext<RecordEntry> context, CancellationToken token)
    {
        var phase = await this.dbContext.Phases.FindAsync(phaseId);
        if (phase == null)
        {
            context.AddFailure("phaseId must be of an existing phase");
            return false;
        }

        var recordDate = IsoDate.Parse(this.parent.Date);
        if (phase.StartDate > recordDate)
        {
            context.AddFailure("phase isn't open yet");
            return false;
        }

        if (phase.EndDate.HasValue && phase.EndDate.Value < recordDate)
        {
            context.AddFailure("phase has already been closed");
            return false;
        }

        return true;
    }
}
