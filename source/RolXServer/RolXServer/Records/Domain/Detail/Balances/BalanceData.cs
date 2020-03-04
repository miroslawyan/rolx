// -----------------------------------------------------------------------
// <copyright file="BalanceData.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using RolXServer.Common.Util;
using RolXServer.Users.DataAccess;

namespace RolXServer.Records.Domain.Detail.Balances
{
    /// <summary>
    /// The data required to evaluate a balance.
    /// </summary>
    internal sealed class BalanceData
    {
        /// <summary>
        /// Gets or sets the nominal work time per day.
        /// </summary>
        public TimeSpan NominalWorkTimePerDay { get; set; }

        /// <summary>
        /// Gets or sets the vacation days per year.
        /// </summary>
        public int VacationDaysPerYear { get; set; }

        /// <summary>
        /// Gets or sets the by-date.
        /// </summary>
        public DateTime ByDate { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public User User { get; set; } = new User();

        /// <summary>
        /// Gets or sets the actual work time in seconds.
        /// </summary>
        public long ActualWorkTimeSeconds { get; set; }

        /// <summary>
        /// Gets or sets the actual work time.
        /// </summary>
        public TimeSpan ActualWorkTime
        {
            get => TimeSpan.FromSeconds(this.ActualWorkTimeSeconds);
            set => this.ActualWorkTimeSeconds = (long)value.TotalSeconds;
        }

        /// <summary>
        /// Gets or sets the paid leave days.
        /// </summary>
        public List<PaidLeaveDay> PaidLeaveDays { get; set; } = new List<PaidLeaveDay>();

        /// <summary>
        /// Gets or sets the vacation days.
        /// </summary>
        public List<PaidLeaveDay> VacationDays { get; set; } = new List<PaidLeaveDay>();

        private TimeSpan NominalWorkTime => this.User.NominalWorkTime(
            new DateRange(this.User.EntryDate!.Value, this.ByDate.AddDays(1)),
            this.NominalWorkTimePerDay);

        private TimeSpan PaidLeaveTime => this.PaidLeaveDays
            .SumOfPaidLeaveTime(this.User, this.NominalWorkTimePerDay);

        private TimeSpan Overtime => this.ActualWorkTime + this.PaidLeaveTime - this.NominalWorkTime;

        private TimeSpan VacationBudget => this.User.VacationBudget(this.ByDate.Year, this.VacationDaysPerYear, this.NominalWorkTimePerDay);

        private TimeSpan NominalWorkTimeAtByDate => this.User.NominalWorkTime(this.ByDate, this.NominalWorkTimePerDay);

        private TimeSpan VacationConsumed => this.VacationDays
            .Where(d => d.Date <= this.ByDate)
            .SumOfPaidLeaveTime(this.User, this.NominalWorkTimePerDay);

        private TimeSpan VacationPlanned => this.VacationDays
            .Where(d => d.Date > this.ByDate)
            .SumOfPaidLeaveTime(this.User, this.NominalWorkTimePerDay);

        private double VacationAvailableDays => (this.VacationBudget - this.VacationConsumed) / this.NominalWorkTimeAtByDate;

        private double VacationPlannedDays => this.VacationPlanned / this.NominalWorkTimeAtByDate;

        /// <summary>
        /// Converts this instance into a balance.
        /// </summary>
        /// <returns>The balance.</returns>
        public Model.Balance ToBalance()
        {
            if (!this.User.EntryDate.HasValue)
            {
                throw new InvalidOperationException("Only users with valid entry date may have a balance.");
            }

            return new Model.Balance
            {
                ByDate = this.ByDate,
                Overtime = this.Overtime,
                VacationAvailableDays = this.VacationAvailableDays,
                VacationPlannedDays = this.VacationPlannedDays,
            };
        }
    }
}
