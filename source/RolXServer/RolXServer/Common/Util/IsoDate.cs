// -----------------------------------------------------------------------
// <copyright file="IsoDate.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;

namespace RolXServer.Common.Util
{
    /// <summary>
    /// Helper and extension methods for ISO-date representation.
    /// </summary>
    public static class IsoDate
    {
        private const string Format = "yyyy-MM-dd";

        /// <summary>
        /// Converts the specified date into a corresponding ISO-date representation.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The ISO-date.</returns>
        public static string ToIsoDate(this DateTime date)
        {
            return date.ToString(Format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the specified date into a corresponding ISO-date representation.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The ISO-date.</returns>
        public static string? ToIsoDate(this DateTime? date)
        {
            return date?.ToString(Format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Tries to parse the specified ISO-date.
        /// </summary>
        /// <param name="isoDate">The ISO-date.</param>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if the parsing succeeded; otherwise <c>false</c>.</returns>
        public static bool TryParse(string isoDate, out DateTime result)
        {
            return DateTime.TryParseExact(
                isoDate,
                Format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeLocal,
                out result);
        }

        /// <summary>
        /// Parses the specified ISO-date.
        /// </summary>
        /// <param name="isoDate">The ISO-date.</param>
        /// <returns>The parsed date.</returns>
        /// <exception cref="FormatException">value is not an ISO-formatted date.</exception>
        public static DateTime Parse(string isoDate)
        {
            if (!TryParse(isoDate, out var result))
            {
                throw new FormatException("value is not an ISO-formatted date");
            }

            return result;
        }

        /// <summary>
        /// Parses the specified ISO-date.
        /// </summary>
        /// <param name="isoDate">The ISO-date.</param>
        /// <returns>The parsed date.</returns>
        /// <exception cref="FormatException">value is not an ISO-formatted date.</exception>
        public static DateTime? ParseNullable(string? isoDate)
        {
            return isoDate != null ? Parse(isoDate) : (DateTime?)null;
        }
    }
}
