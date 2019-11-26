// -----------------------------------------------------------------------
// <copyright file="PhaseExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using RolXServer.Account.DataAccess;

namespace RolXServer.Account.Domain.Detail
{
    /// <summary>
    /// Extension methods for phases.
    /// </summary>
    internal static class PhaseExtensions
    {
        /// <summary>
        /// Resets the full name.
        /// </summary>
        /// <param name="phase">The phase.</param>
        public static void ResetFullName(this Phase phase)
        {
            if (phase.Project is null)
            {
                throw new ArgumentException("Property 'Project' must be set", nameof(phase));
            }

            var project = phase.Project;
            phase.FullName = $"{project.Number}.{phase.Number:D3} - {project.Name} - {phase.Name}";
        }

        /// <summary>
        /// Resets the full name on all specified phases.
        /// </summary>
        /// <param name="phases">The phases.</param>
        public static void ResetFullName(this IEnumerable<Phase> phases)
        {
            foreach (var phase in phases)
            {
                phase.ResetFullName();
            }
        }
    }
}
