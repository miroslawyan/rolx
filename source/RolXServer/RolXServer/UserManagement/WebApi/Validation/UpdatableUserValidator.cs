// -----------------------------------------------------------------------
// <copyright file="UpdatableUserValidator.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using RolXServer.UserManagement.Domain.Model;

namespace RolXServer.UserManagement.WebApi.Validation
{
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
        }
    }
}
