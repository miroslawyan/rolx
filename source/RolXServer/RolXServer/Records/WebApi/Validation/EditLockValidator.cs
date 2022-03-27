// -----------------------------------------------------------------------
// <copyright file="EditLockValidator.cs" company="Christian Ewald">
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
/// Validator for <see cref="EditLock"/> instances.
/// </summary>
public class EditLockValidator : AbstractValidator<EditLock>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EditLockValidator"/> class.
    /// </summary>
    public EditLockValidator()
    {
        this.RuleFor(r => r.Date)
            .NotNull()
            .NotEmpty()
            .SetValidator(new IsoDateValidator<EditLock>());
    }
}
