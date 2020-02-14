// -----------------------------------------------------------------------
// <copyright file="RecordMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using RolXServer.Common.Util;

namespace RolXServer.Records.Domain.Mapping
{
    /// <summary>
    /// Maps records from / to domain.
    /// </summary>
    internal static class RecordMapper
    {
        /// <summary>
        /// Converts to domain.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The domain.</returns>
        public static Model.Record ToDomain(this DataAccess.Record entity)
        {
            return new Model.Record
            {
                Date = entity.Date,
                UserId = entity.UserId,
                Entries = entity.Entries,
            };
        }

        /// <summary>
        /// Converts to entity.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>
        /// The entity.
        /// </returns>
        public static DataAccess.Record ToEntity(this Model.Record domain)
        {
            return new DataAccess.Record
            {
                Date = domain.Date,
                UserId = domain.UserId,
                Entries = domain.Entries,
            };
        }

        /// <summary>
        /// Converts to all domains in the specified range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="range">The range.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// The domains.
        /// </returns>
        public static IEnumerable<Model.Record> ToDomainsIn(this IEnumerable<DataAccess.Record> entities, DateRange range, Guid userId)
        {
            var current = range.Begin;
            foreach (var domain in entities.Select(e => e.ToDomain()))
            {
                foreach (var empty in new DateRange(current, domain.Date).ToEmptyDomain(userId))
                {
                    yield return empty;
                }

                yield return domain;

                current = domain.Date.AddDays(1);
            }

            foreach (var empty in new DateRange(current, range.End).ToEmptyDomain(userId))
            {
                yield return empty;
            }
        }

        private static IEnumerable<Model.Record> ToEmptyDomain(this DateRange range, Guid userId)
        {
            return range.Days.Select(d => new Model.Record
            {
                Date = d,
                UserId = userId,
            });
        }
    }
}
