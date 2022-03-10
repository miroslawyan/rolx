// -----------------------------------------------------------------------
// <copyright file="DateRange.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Common.Util;

/// <summary>
/// A range of dates (begin..end].
/// </summary>
public struct DateRange : IEquatable<DateRange>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateRange"/> struct.
    /// </summary>
    /// <param name="begin">The begin.</param>
    /// <param name="end">The end.</param>
    public DateRange(DateTime begin, DateTime end)
    {
        if (begin.Date != begin)
        {
            throw new ArgumentException("not a date", nameof(begin));
        }

        if (end.Date != end)
        {
            throw new ArgumentException("not a date", nameof(end));
        }

        if (end < begin)
        {
            throw new ArgumentException("end must be after begin", nameof(end));
        }

        this.Begin = begin;
        this.End = end;
    }

    /// <summary>
    /// Gets the begin.
    /// </summary>
    public DateTime Begin { get; }

    /// <summary>
    /// Gets the end.
    /// </summary>
    public DateTime End { get; }

    /// <summary>
    /// Gets the days in this range.
    /// </summary>
    public IEnumerable<DateTime> Days
    {
        get
        {
            var begin = this.Begin; // CS1674 otherwise
            return Enumerable.Range(0, (this.End - this.Begin).Days)
                .Select(i => begin.AddDays(i));
        }
    }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(DateRange left, DateRange right)
        => left.Begin == right.Begin && left.End == right.End;

    public static bool operator !=(DateRange left, DateRange right)
        => left.Begin != right.Begin || left.End != right.End;

    /// <summary>
    /// Creates a date-range for the specified month.
    /// </summary>
    /// <param name="month">The month.</param>
    /// <returns>The date-range.</returns>
    public static DateRange ForMonth(DateTime month)
    {
        var begin = new DateTime(month.Year, month.Month, 1);

        var nextMonth = month.AddMonths(1);
        var end = new DateTime(nextMonth.Year, nextMonth.Month, 1);

        return new DateRange(begin, end);
    }

    /// <summary>
    /// Creates a date-range for the specified year.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <returns>The date-range.</returns>
    public static DateRange ForYear(int year)
    {
        var begin = new DateTime(year, 1, 1);
        var end = new DateTime(year + 1, 1, 1);

        return new DateRange(begin, end);
    }

    /// <summary>
    /// Safely creates a new <see cref="DateRange"/> instance.
    /// </summary>
    /// <param name="begin">The begin.</param>
    /// <param name="end">The end.</param>
    /// <returns>The created instance.</returns>
    public static DateRange CreateSafe(DateTime begin, DateTime end)
        => new DateRange(begin, end > begin ? end : begin);

    /// <summary>
    /// Determines whether this range contains the specified date.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns>
    ///   <c>true</c> if this range contains the specified date.
    /// </returns>
    public bool Contains(DateTime date) => this.Begin <= date && this.End > date;

    /// <inheritdoc/>
    public override bool Equals(object? other)
    {
        if (other is DateRange otherRange)
        {
            return this == otherRange;
        }

        return false;
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(DateRange other) => this == other;

    /// <inheritdoc/>
    public override int GetHashCode() => (this.Begin, this.End).GetHashCode();
}
