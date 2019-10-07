// -----------------------------------------------------------------------
// <copyright file="Customer.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Account.DataAccess
{
    /// <summary>
    /// A customer we're have projects with (and paying the bills).
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
