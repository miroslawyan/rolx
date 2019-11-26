// -----------------------------------------------------------------------
// <copyright file="IsoDateValidator.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation.Validators;

namespace RolXServer.Common.Util
{
    /// <summary>
    /// Validator for ISO-formatted dates.
    /// </summary>
    public sealed class IsoDateValidator : PropertyValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsoDateValidator"/> class.
        /// </summary>
        public IsoDateValidator()
            : base("{PropertyName} must be a valid, ISO-formatted date.")
        {
        }

        /// <summary>
        /// Returns true if the validated context is valid.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        ///   <c>true</c> if the specified context is valid; otherwise, <c>false</c>.
        /// </returns>
        protected override bool IsValid(PropertyValidatorContext context)
        {
            return context.PropertyValue is string value
                && IsoDate.TryParse(value, out var unused);
        }
    }
}
