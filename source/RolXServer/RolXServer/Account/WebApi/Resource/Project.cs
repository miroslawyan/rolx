// -----------------------------------------------------------------------
// <copyright file="Project.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace RolXServer.Account.WebApi.Resource
{
    /// <summary>
    /// A project we are working on.
    /// </summary>
    public class Project
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

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the date until this project is open.
        /// </summary>
        public DateTime? OpenUntil { get; set; }
    }
}
