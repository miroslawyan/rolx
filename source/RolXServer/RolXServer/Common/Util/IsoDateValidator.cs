// -----------------------------------------------------------------------
// <copyright file="IsoDateValidator.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using FluentValidation.Validators;

namespace RolXServer.Common.Util;

/// <summary>
/// Validator for ISO-formatted date properties.
/// </summary>
/// <typeparam name="T">The type owning the property to validate.</typeparam>
public sealed class IsoDateValidator<T> : PropertyValidator<T, string?>
{
    /// <inheritdoc/>
    public override string Name => nameof(IsoDateValidator<T>);

    /// <inheritdoc/>
    public override bool IsValid(ValidationContext<T> context, string? value)
        => value is null || IsoDate.TryParse(value, out var _);

    /// <inheritdoc/>
    protected override string GetDefaultMessageTemplate(string errorCode)
      => "{PropertyName} must be a valid, ISO-formatted date.";
}
