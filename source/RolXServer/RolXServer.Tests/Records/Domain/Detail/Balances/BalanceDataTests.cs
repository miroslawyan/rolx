// -----------------------------------------------------------------------
// <copyright file="BalanceDataTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using FluentAssertions;
using NUnit.Framework;
using RolXServer.Users.DataAccess;

namespace RolXServer.Records.Domain.Detail.Balances
{
    public sealed class BalanceDataTests
    {
        private BalanceData sut = null!;

        [SetUp]
        public void SetUp()
        {
            this.sut = new BalanceData
            {
                NominalWorkTimePerDay = TimeSpan.FromHours(8),
                User = new User { EntryDate = new DateTime(2020, 3, 2) },
                ByDate = new DateTime(2020, 3, 8),
            };
        }

        [Test]
        public void Overtime_SimpleWeek()
        {
            this.sut.ActualWorkTime = TimeSpan.FromHours(40);

            this.sut.ToBalance()
                .Overtime.Should().Be(default);
        }

        [Test]
        public void Overtime_UserPartiallyActive()
        {
            this.sut.User.EntryDate = new DateTime(2020, 3, 6);
            this.sut.ActualWorkTime = TimeSpan.FromHours(8);

            this.sut.ToBalance()
                .Overtime.Should().Be(default);
        }

        [Test]
        public void Overtime_OneFullPaidLeave()
        {
            this.sut.ActualWorkTime = TimeSpan.FromHours(32);
            this.sut.PaidLeaveDays = new List<PaidLeaveDay>
            {
                new PaidLeaveDay { Date = new DateTime(2020, 3, 4), ActualWorkTime = default },
            };

            this.sut.ToBalance()
                .Overtime.Should().Be(default);
        }

        [Test]
        public void Overtime_OnePartialPaidLeave()
        {
            this.sut.ActualWorkTime = TimeSpan.FromHours(34);
            this.sut.PaidLeaveDays = new List<PaidLeaveDay>
            {
                new PaidLeaveDay { Date = new DateTime(2020, 3, 4), ActualWorkTime = TimeSpan.FromHours(2) },
            };

            this.sut.ToBalance()
                .Overtime.Should().Be(default);
        }

        [Test]
        public void Overtime_FullWeekPaidLeave()
        {
            this.sut.ActualWorkTime = default;
            this.sut.PaidLeaveDays = new List<PaidLeaveDay>
            {
                new PaidLeaveDay { Date = new DateTime(2020, 3, 2), ActualWorkTime = default },
                new PaidLeaveDay { Date = new DateTime(2020, 3, 3), ActualWorkTime = default },
                new PaidLeaveDay { Date = new DateTime(2020, 3, 4), ActualWorkTime = default },
                new PaidLeaveDay { Date = new DateTime(2020, 3, 5), ActualWorkTime = default },
                new PaidLeaveDay { Date = new DateTime(2020, 3, 6), ActualWorkTime = default },
            };

            this.sut.ToBalance()
                .Overtime.Should().Be(default);
        }
    }
}
