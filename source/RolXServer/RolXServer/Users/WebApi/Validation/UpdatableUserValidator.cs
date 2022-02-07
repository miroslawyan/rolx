// -----------------------------------------------------------------------
// <copyright file="UpdatableUserValidator.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using RolXServer.Common.Util;
using RolXServer.Users.WebApi.Resource;

namespace RolXServer.Users.WebApi.Validation;

/// <summary>
/// Validator for <see cref="UpdatableUser"/> instances.
/// </summary>
public sealed class UpdatableUserValidator : AbstractValidator<UpdatableUser>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatableUserValidator"/> class.
    /// </summary>
    public UpdatableUserValidator()
    {
        this.RuleFor(u => u.Role).IsInEnum();

        this.RuleFor(u => u.EntryDate)
            .NotEmpty()
            .SetValidator(new IsoDateValidator<UpdatableUser>())
            .Unless(u => u.EntryDate == null);

        this.RuleFor(u => u.LeftDate)
            .NotEmpty()
            .SetValidator(new IsoDateValidator<UpdatableUser>())
            .Unless(u => u.LeftDate == null);

        this.RuleFor(u => u.LeftDate)
            .Null()
            .Unless(u => u.EntryDate != null);

        this.RuleFor(u => u.LeftDate)
            .GreaterThan(u => u.EntryDate);
    }
}
