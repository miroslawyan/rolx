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
        private static readonly TimeSpan BudgetMin = TimeSpan.FromMinutes(1);

        /// <summary>
        /// Sanitizes the specified phase.
        /// </summary>
        /// <param name="phase">The phase.</param>
        public static void Sanitize(this Phase phase)
        {
            phase.ResetFullName();
            phase.ClearEmptyBudget();
        }

        /// <summary>
        /// Sanitizes the specified phases.
        /// </summary>
        /// <param name="phases">The phases.</param>
        public static void Sanitize(this IEnumerable<Phase> phases)
        {
            foreach (var phase in phases)
            {
                phase.Sanitize();
            }
        }

        private static void ResetFullName(this Phase phase)
        {
            if (phase.Project is null)
            {
                throw new ArgumentException("Property 'Project' must be set", nameof(phase));
            }

            var project = phase.Project;
            phase.FullName = $"{project.Number}.{phase.Number:D3} - {project.Name} - {phase.Name}";
        }

        private static void ClearEmptyBudget(this Phase phase)
        {
            if ((phase.Budget ?? default) < BudgetMin)
            {
                phase.Budget = null;
            }
        }
    }
}
