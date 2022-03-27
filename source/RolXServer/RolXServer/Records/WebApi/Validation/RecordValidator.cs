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
using RolXServer.Records.Domain;
using RolXServer.Records.WebApi.Resource;

namespace RolXServer.Records.WebApi.Validation;

/// <summary>
/// Validator for <see cref="Record"/> instances.
/// </summary>
public sealed class RecordValidator : AbstractValidator<Record>
{
    private readonly RolXContext rolXContext;
    private readonly IEditLockService editLockService;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecordValidator" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="editLockService">The edit lock service.</param>
    public RecordValidator(RolXContext dbContext, IEditLockService editLockService)
    {
        this.rolXContext = dbContext;
        this.editLockService = editLockService;

        this.RuleFor(r => r.Date)
            .NotNull()
            .NotEmpty()
            .SetValidator(new IsoDateValidator<Record>())
            .MustAsync(this.BeAfterEditLock);

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

    private async Task<bool> BeAfterEditLock(Record candidate, string isoDate, ValidationContext<Record> context, CancellationToken cancellationToken)
    {
        var editLock = await this.editLockService.Get();
        if (!IsoDate.TryParse(isoDate, out var date))
        {
            // The IsoDateValidator will take care of this case.
            return true;
        }

        if (date < editLock.Date)
        {
            context.AddFailure($"Editing before {editLock.Date.ToIsoDate()} is locked.");
            return false;
        }

        return true;
    }
}
