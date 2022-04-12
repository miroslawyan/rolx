// -----------------------------------------------------------------------
// <copyright file="IsoDate.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;

namespace RolXServer.Common.Util;

/// <summary>
/// Helper and extension methods for ISO-date representation.
/// </summary>
public static class IsoDate
{
    private const string FullFormat = "yyyy-MM-dd";
    private const string MonthFormat = "yyyy-MM";

    /// <summary>
    /// Converts the specified date into a corresponding ISO-date representation.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns>The ISO-date.</returns>
    public static string ToIsoDate(this DateOnly date)
        => date.ToString(FullFormat, CultureInfo.InvariantCulture);

    /// <summary>
    /// Tries to parse the specified ISO-date.
    /// </summary>
    /// <param name="isoDate">The ISO-date.</param>
    /// <param name="result">The result.</param>
    /// <returns><c>true</c> if the parsing succeeded; otherwise <c>false</c>.</returns>
    public static bool TryParse(string isoDate, out DateOnly result)
        => DateOnly.TryParseExact(
            isoDate,
            FullFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out result);

    /// <summary>
    /// Tries to parse the specified ISO-date.
    /// </summary>
    /// <param name="isoDate">The ISO-date.</param>
    /// <param name="result">The result.</param>
    /// <returns><c>true</c> if the parsing succeeded; otherwise <c>false</c>.</returns>
    public static bool TryParseNullable(string? isoDate, out DateOnly? result)
    {
        if (isoDate == null)
        {
            result = null;
            return true;
        }

        var returnValue = TryParse(isoDate, out var parsed);

        result = parsed;
        return returnValue;
    }

    /// <summary>
    /// Parses the specified ISO-date.
    /// </summary>
    /// <param name="isoDate">The ISO-date.</param>
    /// <returns>The parsed date.</returns>
    /// <exception cref="FormatException">value is not an ISO-formatted date.</exception>
    public static DateOnly Parse(string isoDate)
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
    public static DateOnly? ParseNullable(string? isoDate)
        => isoDate != null ? Parse(isoDate) : null;

    /// <summary>
    /// Tries to parse the specified ISO-formatted month.
    /// </summary>
    /// <param name="isoMonth">The ISO-month.</param>
    /// <param name="result">The result.</param>
    /// <returns><c>true</c> if the parsing succeeded; otherwise <c>false</c>.</returns>
    public static bool TryParseMonth(string isoMonth, out DateOnly result)
        => DateOnly.TryParseExact(
            isoMonth,
            MonthFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out result);
}
