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
public record struct DateRange
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateRange"/> class.
    /// </summary>
    /// <param name="begin">The begin.</param>
    /// <param name="end">The end.</param>
    public DateRange(DateOnly begin, DateOnly end)
    {
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
    public DateOnly Begin { get; }

    /// <summary>
    /// Gets the end.
    /// </summary>
    public DateOnly End { get; }

    /// <summary>
    /// Gets the days in this range.
    /// </summary>
    public IEnumerable<DateOnly> Days
    {
        get
        {
            var begin = this.Begin; // CS1674 otherwise
            var numberOfDays = (this.End.ToDateTime(default) - begin.ToDateTime(default)).Days;

            return Enumerable.Range(0, numberOfDays)
                .Select(i => begin.AddDays(i));
        }
    }

    /// <summary>
    /// Creates a date-range for the specified month.
    /// </summary>
    /// <param name="month">The month.</param>
    /// <returns>The date-range.</returns>
    public static DateRange ForMonth(DateOnly month)
    {
        var begin = new DateOnly(month.Year, month.Month, 1);

        var nextMonth = month.AddMonths(1);
        var end = new DateOnly(nextMonth.Year, nextMonth.Month, 1);

        return new DateRange(begin, end);
    }

    /// <summary>
    /// Creates a date-range for the specified year.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <returns>The date-range.</returns>
    public static DateRange ForYear(int year)
    {
        var begin = new DateOnly(year, 1, 1);
        var end = new DateOnly(year + 1, 1, 1);

        return new DateRange(begin, end);
    }

    /// <summary>
    /// Safely creates a new <see cref="DateRange"/> instance.
    /// </summary>
    /// <param name="begin">The begin.</param>
    /// <param name="end">The end.</param>
    /// <returns>The created instance.</returns>
    public static DateRange CreateSafe(DateOnly begin, DateOnly end)
        => new DateRange(begin, end > begin ? end : begin);

    /// <summary>
    /// Determines whether this range includes the specified date.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns>
    ///   <c>true</c> if this range contains the specified date.
    /// </returns>
    public bool Includes(DateOnly date) => this.Begin <= date && this.End > date;

    /// <summary>
    /// Creates a range including this range and the specified date.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns>The extended range.</returns>
    public DateRange Include(DateOnly date)
        => this.Include(new DateRange(date, date.AddDays(1)));

    /// <summary>
    /// Creates a range including both, this range and the other one.
    /// </summary>
    /// <param name="other">The other range.</param>
    /// <returns>
    /// The extended range.
    /// </returns>
    public DateRange Include(DateRange other)
        => new(
            this.Begin < other.Begin ? this.Begin : other.Begin,
            this.End > other.End ? this.End : other.End);

    /// <summary>
    /// Intersects this range with the specified other range.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>The intersecting range or null, there is none.</returns>
    public DateRange? Intersect(DateRange other)
    {
        if (this.End > other.Begin || other.End > this.Begin)
        {
            return new(
                this.Begin > other.Begin ? this.Begin : other.Begin,
                this.End < other.End ? this.End : other.End);
        }

        return null;
    }
}
