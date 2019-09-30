// -----------------------------------------------------------------------
// <copyright file="Record.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace RolXServer.WorkRecord.Domain.Model
{
    /// <summary>
    /// A record for a day.
    /// </summary>
    public sealed class Record
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
