// -----------------------------------------------------------------------
// <copyright file="UserExtensionsTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;

using FluentAssertions;
using NUnit.Framework;
using RolXServer.Common.Util;
using RolXServer.Users.DataAccess;

namespace RolXServer.Records.Domain.Detail
{
    public sealed class UserExtensionsTests
    {
        private static readonly TimeSpan NominalWorkTimePerDay = TimeSpan.FromHours(8);

        [Test]
        public void AnInfoForEachDayInRange()
        {
            new User()
                .DayInfos(new DateRange(new DateTime(2020, 1, 1), new DateTime(2020, 1, 11)), NominalWorkTimePerDay)
                .Count().Should().Be(10);
        }

        [TestCase(2020, 1, 4)]
        [TestCase(2020, 1, 5)]
        [TestCase(2020, 5, 23)]
        [TestCase(2020, 5, 24)]
        public void DayType_Weekend(int year, int month, int day)
        {
            var begin = new DateTime(year, month, day);
            var end = begin.AddDays(1);

            new User()
                .DayInfos(new DateRange(begin, end), NominalWorkTimePerDay)
                .Single()
                .DayType.Should().Be(DayType.Weekend);
        }

        [TestCase(2020, 1, 3)]
        [TestCase(2020, 5, 18)]
        [TestCase(2020, 5, 19)]
        [TestCase(2020, 5, 20)]
        [TestCase(2020, 5, 22)]
        public void DayType_Workday(int year, int month, int day)
        {
            var begin = new DateTime(year, month, day);
            var end = begin.AddDays(1);

            new User()
                .DayInfos(new DateRange(begin, end), NominalWorkTimePerDay)
                .Single()
                .DayType.Should().Be(DayType.Workday);
        }

        [TestCase(2020, 1, 1)]
        [TestCase(2020, 1, 2)]
        [TestCase(2020, 5, 21)]
        public void DayType_Holyday(int year, int month, int day)
        {
            var begin = new DateTime(year, month, day);
            var end = begin.AddDays(1);

            new User()
                .DayInfos(new DateRange(begin, end), NominalWorkTimePerDay)
                .Single()
                .DayType.Should().Be(DayType.Holiday);
        }

        [TestCase(2020, 1, 1, "Neujahr")]
        [TestCase(2020, 1, 2, "Berchtoldstag")]
        [TestCase(2020, 5, 21, "Auffahrt")]
        public void DayName_Holiday(int year, int month, int day, string name)
        {
            var begin = new DateTime(year, month, day);
            var end = begin.AddDays(1);

            new User()
                .DayInfos(new DateRange(begin, end), NominalWorkTimePerDay)
                .Single()
                .DayName.Should().Be(name);
        }

        [TestCase(2020, 1, 3)]
        [TestCase(2020, 1, 4)]
        [TestCase(2020, 1, 5)]
        [TestCase(2020, 5, 18)]
        [TestCase(2020, 5, 19)]
        [TestCase(2020, 5, 20)]
        [TestCase(2020, 5, 22)]
        [TestCase(2020, 5, 23)]
        [TestCase(2020, 5, 24)]
        public void DayName_NonHoliday(int year, int month, int day)
        {
            var begin = new DateTime(year, month, day);
            var end = begin.AddDays(1);

            new User()
                .DayInfos(new DateRange(begin, end), NominalWorkTimePerDay)
                .Single()
                .DayName.Should().BeEmpty();
        }

        [TestCase(2020, 1, 3)]
        [TestCase(2020, 5, 18)]
        [TestCase(2020, 5, 19)]
        [TestCase(2020, 5, 20)]
        [TestCase(2020, 5, 22)]
        public void NominalWorkTime_Workday(int year, int month, int day)
        {
            var begin = new DateTime(year, month, day);
            var end = begin.AddDays(1);

            new User()
                .DayInfos(new DateRange(begin, end), NominalWorkTimePerDay)
                .Single()
                .NominalWorkTime.Should().Be(TimeSpan.FromHours(8));
        }

        [TestCase(2020, 1, 1)]
        [TestCase(2020, 1, 2)]
        [TestCase(2020, 1, 4)]
        [TestCase(2020, 1, 5)]
        [TestCase(2020, 5, 21)]
        [TestCase(2020, 5, 23)]
        [TestCase(2020, 5, 24)]
        public void NominalWorkTime_NonWorkday(int year, int month, int day)
        {
            var begin = new DateTime(year, month, day);
            var end = begin.AddDays(1);

            new User()
                .DayInfos(new DateRange(begin, end), NominalWorkTimePerDay)
                .Single()
                .NominalWorkTime.Should().Be(default);
        }

        [Test]
        public void PartTime_Before()
        {
            var user = new User();
            user.Settings.Add(new UserSetting
            {
                StartDate = new DateTime(2020, 1, 1),
                PartTimeFactor = 0.5,
            });

            user.NominalWorkTime(new DateRange(new DateTime(2020, 2, 1), new DateTime(2020, 2, 8)), NominalWorkTimePerDay)
                .Should().Be(TimeSpan.FromHours(20));
        }

        [Test]
        public void PartTime_Between()
        {
            var user = new User();
            user.Settings.Add(new UserSetting
            {
                StartDate = new DateTime(2020, 2, 5),
                PartTimeFactor = 0.5,
            });

            user.NominalWorkTime(new DateRange(new DateTime(2020, 2, 1), new DateTime(2020, 2, 8)), NominalWorkTimePerDay)
                .Should().Be(TimeSpan.FromHours(28));
        }

        [Test]
        public void PartTime_After()
        {
            var user = new User();
            user.Settings.Add(new UserSetting
            {
                StartDate = new DateTime(2020, 8, 1),
                PartTimeFactor = 0.5,
            });

            user.NominalWorkTime(new DateRange(new DateTime(2020, 2, 1), new DateTime(2020, 2, 8)), NominalWorkTimePerDay)
                .Should().Be(TimeSpan.FromHours(40));
        }

        [TestCase(1, 2016)]
        [TestCase(2, 3622.4)]
        [TestCase(5, 9284.8)]
        [TestCase(10, 18168.0)]
        [TestCase(20, 36344.0)]
        [TestCase(50, 90944.0)]
        public void NominalWorkTime_LongTime(int years, double expectedHours)
        {
            var begin = new DateTime(2000, 1, 1);
            var end = begin.AddYears(years);

            var user = new User();

            for (var i = 0; i < years; ++i)
            {
                user.Settings.Add(new UserSetting
                {
                    StartDate = begin.AddYears(i),
                    PartTimeFactor = i % 2 == 0 ? 1.0 : 0.8,
                });
            }

            user.NominalWorkTime(new DateRange(begin, end), NominalWorkTimePerDay)
                .TotalHours.Should().Be(expectedHours);
        }
    }
}
