// -----------------------------------------------------------------------
// <copyright file="IBalanceService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using RolXServer.Records.Domain.Model;

namespace RolXServer.Records.Domain
{
    /// <summary>
    /// Provides access to balances.
    /// </summary>
    public interface IBalanceService
    {
        /// <summary>
        /// Gets the balance of the specified user by the specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The balance.</returns>
        Task<Balance> GetByDate(DateTime date, Guid userId);
    }
}
