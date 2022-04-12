// -----------------------------------------------------------------------
// <copyright file="BalanceDataTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Users.DataAccess;

namespace RolXServer.Records.Domain.Detail.Balances;

public sealed class BalanceDataTests
{
    private BalanceData sut = null!;

    [SetUp]
    public void SetUp()
    {
        this.sut = new BalanceData
        {
            NominalWorkTimePerDay = TimeSpan.FromHours(8),
            User = new User { EntryDate = new DateOnly(2020, 3, 2) },
            ByDate = new DateOnly(2020, 3, 8),
            VacationDaysPerYear = 25,
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
        this.sut.User.EntryDate = new DateOnly(2020, 3, 6);
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
                new PaidLeaveDay { Date = new DateOnly(2020, 3, 4), ActualWorkTime = default },
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
                new PaidLeaveDay { Date = new DateOnly(2020, 3, 4), ActualWorkTime = TimeSpan.FromHours(2) },
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
                new PaidLeaveDay { Date = new DateOnly(2020, 3, 2), ActualWorkTime = default },
                new PaidLeaveDay { Date = new DateOnly(2020, 3, 3), ActualWorkTime = default },
                new PaidLeaveDay { Date = new DateOnly(2020, 3, 4), ActualWorkTime = default },
                new PaidLeaveDay { Date = new DateOnly(2020, 3, 5), ActualWorkTime = default },
                new PaidLeaveDay { Date = new DateOnly(2020, 3, 6), ActualWorkTime = default },
            };

        this.sut.ToBalance()
            .Overtime.Should().Be(default);
    }

    [Test]
    public void VacationAvailableDays_AllWell()
    {
        this.sut.User.EntryDate = new DateOnly(2020, 1, 1);
        this.sut.ByDate = new DateOnly(2020, 1, 3);

        this.sut.ToBalance()
            .VacationAvailableDays.Should().Be(25);
    }

    [Test]
    public void VacationAvailableDays_ByDateOnWeekend()
    {
        this.sut.User.EntryDate = new DateOnly(2020, 1, 1);
        this.sut.ByDate = new DateOnly(2020, 1, 5); // was a Sunday

        this.sut.ToBalance()
            .VacationAvailableDays.Should().Be(25);
    }

    [Test]
    public void VacationPlannedDays_AllWell()
    {
        this.sut.User.EntryDate = new DateOnly(2020, 1, 1);
        this.sut.ByDate = new DateOnly(2020, 1, 3);

        this.sut.ToBalance()
            .VacationPlannedDays.Should().Be(0);
    }

    [Test]
    public void VacationPlannedDays_ByDateOnWeekend()
    {
        this.sut.User.EntryDate = new DateOnly(2020, 1, 1);
        this.sut.ByDate = new DateOnly(2020, 1, 5); // was a Sunday

        this.sut.ToBalance()
            .VacationPlannedDays.Should().Be(0);
    }
}
