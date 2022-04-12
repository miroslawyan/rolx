// -----------------------------------------------------------------------
// <copyright file="IsoDateTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Common.Util;

/// <summary>
/// Unit tests for the <see cref="IsoDate"/> class.
/// </summary>
public sealed class IsoDateTests
{
    [TestCase(2019, 11, 23, "2019-11-23")]
    [TestCase(1914, 1, 3, "1914-01-03")]
    public void FromDateTimeToIsoDate(int year, int month, int day, string expected)
    {
        new DateOnly(year, month, day).ToIsoDate()
            .Should().Be(expected);
    }

    [TestCase("2019-11-23", 2019, 11, 23)]
    [TestCase("1914-01-03", 1914, 1, 3)]
    public void Parse(string source, int year, int month, int day)
    {
        IsoDate.Parse(source)
            .Should().Be(new DateOnly(year, month, day));
    }

    [Test]
    public void ParseThrowsOnWrongFormat()
    {
        "23.11.2013".Invoking(s => IsoDate.Parse(s))
            .Should().Throw<FormatException>()
            .WithMessage("value is not an ISO-formatted date");
    }

    [TestCase("2019-11-23", 2019, 11, 23)]
    [TestCase("1914-01-03", 1914, 1, 3)]
    public void ParseNullable(string? source, int year, int month, int day)
    {
        IsoDate.ParseNullable(source)
            .Should().Be(new DateOnly(year, month, day));
    }

    [Test]
    public void ParseNullableNull()
    {
        IsoDate.ParseNullable(null)
            .Should().BeNull();
    }
}
