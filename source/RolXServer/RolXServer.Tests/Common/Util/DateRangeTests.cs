// -----------------------------------------------------------------------
// <copyright file="DateRangeTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Common.Util;

/// <summary>
/// Unit tests for the <see cref="DateRange"/> class.
/// </summary>
public sealed class DateRangeTests
{
    [Test]
    public void TestDays()
    {
        var start = new DateOnly(2022, 3, 1);
        for (var i = 0; i < 40; ++i)
        {
            var sut = new DateRange(new DateOnly(2022, 3, 1), start.AddDays(i));
            sut.Days.Count().Should().Be(i);
        }
    }
}
