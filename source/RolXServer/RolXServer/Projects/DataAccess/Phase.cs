// -----------------------------------------------------------------------
// <copyright file="Phase.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolXServer.Projects.DataAccess
{
    /// <summary>
    /// A phase of a project.
    /// </summary>
    public sealed class Phase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is billable.
        /// </summary>
        public bool IsBillable { get; set; }

        /// <summary>
        /// Gets or sets the time budget in seconds.
        /// </summary>
        public TimeSpan? Budget { get; set; }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        public Project? Project { get; set; }
    }
}
