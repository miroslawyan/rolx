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

namespace RolXServer.Records.Domain.Mapping
{
    /// <summary>
    /// Maps records from / to domain.
    /// </summary>
    internal static class RecordMapper
    {
        /// <summary>
        /// Maps the specified day-informations into records.
        /// </summary>
        /// <param name="dayInfos">The day infos.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="entities">The entities.</param>
        /// <returns>The records.</returns>
        public static IEnumerable<Model.Record> ToRecords(
            this IEnumerable<Model.DayInfo> dayInfos,
            Guid userId,
            ICollection<DataAccess.Record> entities)
        {
            return dayInfos.Select(i => i.ToRecord(userId, entities.FirstOrDefault(e => e.Date == i.Date)));
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
                Date = domain.DayInfo.Date,
                UserId = domain.UserId,
                PaidLeaveType = domain.PaidLeaveType,
                PaidLeaveReason = domain.PaidLeaveReason,
                Entries = domain.Entries,
            };
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="entity">The entity.</param>
        public static void Update(this Model.Record domain, DataAccess.Record entity)
        {
            entity.PaidLeaveType = domain.PaidLeaveType;
            entity.PaidLeaveReason = domain.PaidLeaveReason;

            entity.Entries.Clear();
            entity.Entries = domain.Entries;
        }

        private static Model.Record ToRecord(this Model.DayInfo dayInfo, Guid userId, DataAccess.Record? entity)
        {
            var record = new Model.Record(dayInfo)
            {
                UserId = userId,
            };

            if (entity != null)
            {
                record.PaidLeaveType = entity.PaidLeaveType;
                record.PaidLeaveReason = entity.PaidLeaveReason;
                record.Entries = entity.Entries;
            }

            return record;
        }
    }
}
