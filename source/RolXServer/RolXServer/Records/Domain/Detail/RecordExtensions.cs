// -----------------------------------------------------------------------
// <copyright file="RecordExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Records.Domain.Model;

namespace RolXServer.Records.Domain.Detail
{
    /// <summary>
    /// Extensions methods for <see cref="Record"/> instances.
    /// </summary>
    internal static class RecordExtensions
    {
        /// <summary>
        /// Sanitizes the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        public static void Sanitize(this Record record)
        {
            record.RemoveEmptyEntries();
        }

        private static void RemoveEmptyEntries(this Record record)
        {
            record.Entries.RemoveAll(e => e.Duration == default);
        }
    }
}
