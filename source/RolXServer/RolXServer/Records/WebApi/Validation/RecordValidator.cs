// -----------------------------------------------------------------------
// <copyright file="RecordValidator.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using RolXServer.Common.Util;
using RolXServer.Projects;
using RolXServer.Records.WebApi.Resource;

namespace RolXServer.Records.WebApi.Validation;

/// <summary>
/// Validator for <see cref="Record"/> instances.
/// </summary>
public sealed class RecordValidator : AbstractValidator<Record>
{
    private readonly RolXContext rolXContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecordValidator" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public RecordValidator(RolXContext dbContext)
    {
        this.rolXContext = dbContext;

        this.RuleFor(r => r.Date)
            .NotNull()
            .NotEmpty()
            .SetValidator(new IsoDateValidator<Record>());

        this.RuleForEach(r => r.Entries)
            .SetValidator(r => new RecordEntryValidator(r, this.rolXContext));

        this.RuleFor(r => r.PaidLeaveReason)
            .NotNull()
            .NotEmpty()
            .Unless(r => r.PaidLeaveType != PaidLeaveType.Other);

        this.RuleFor(r => r.PaidLeaveType)
            .Must(NotHaveOvertime)
            .Unless(r => !r.PaidLeaveType.HasValue);
    }

    private static bool NotHaveOvertime(Record candidate, PaidLeaveType? type, ValidationContext<Record> context)
    {
        var workTime = candidate.Entries
            .Select(e => e.Duration)
            .DefaultIfEmpty(0)
            .Sum();

        if (workTime > candidate.NominalWorkTime)
        {
            context.AddFailure("It's not allowed to do overtime on days with paid leave.");
            return false;
        }

        return true;
    }
}
