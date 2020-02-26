// -----------------------------------------------------------------------
// <copyright file="RecordValidatorTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using FluentValidation.TestHelper;
using NUnit.Framework;
using RolXServer.Records.WebApi.Resource;

namespace RolXServer.Records.WebApi.Validation
{
    /// <summary>
    /// Unit tests for the <see cref="RecordValidator"/>.
    /// </summary>
    public sealed class RecordValidatorTests
    {
        private RecordValidator sut = null!;
        private RolXContext context = null!;

        [SetUp]
        public void SetUp()
        {
            this.context = InMemory.ContextFactory()();
            this.sut = new RecordValidator(this.context);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }

        [Test]
        public void PaidLeaveReason_MustBeNonNullWhenTypeIsOther()
        {
            var record = new Record
            {
                PaidLeaveType = PaidLeaveType.Other,
                PaidLeaveReason = null,
            };

            this.sut.ShouldHaveValidationErrorFor(record => record.PaidLeaveReason, record);
        }

        [Test]
        public void PaidLeaveReason_MustBeNonEmptyWhenTypeIsOther()
        {
            var record = new Record
            {
                PaidLeaveType = PaidLeaveType.Other,
                PaidLeaveReason = string.Empty,
            };

            this.sut.ShouldHaveValidationErrorFor(record => record.PaidLeaveReason, record);
        }

        [Test]
        public void PaidLeaveReason_FineWhenNonEmpty()
        {
            var record = new Record
            {
                PaidLeaveType = PaidLeaveType.Other,
                PaidLeaveReason = "The rain in Spain.",
            };

            this.sut.ShouldNotHaveValidationErrorFor(record => record.PaidLeaveReason, record);
        }

        [TestCase(null)]
        [TestCase(PaidLeaveType.Vacation)]
        [TestCase(PaidLeaveType.Sickness)]
        [TestCase(PaidLeaveType.MilitaryService)]
        public void PaidLeaveReason_FineWhenNullForType(PaidLeaveType? type)
        {
            var record = new Record
            {
                PaidLeaveType = type,
                PaidLeaveReason = null,
            };

            this.sut.ShouldNotHaveValidationErrorFor(record => record.PaidLeaveReason, record);
        }

        [TestCase(PaidLeaveType.Vacation)]
        [TestCase(PaidLeaveType.Sickness)]
        [TestCase(PaidLeaveType.MilitaryService)]
        [TestCase(PaidLeaveType.Other)]
        public void PaidLeaveType_IsNotAllowedIfDoingOvertime(PaidLeaveType type)
        {
            var record = new Record
            {
                PaidLeaveType = type,
                NominalWorkTime = 1000,
                Entries = new List<RecordEntry>
                {
                    new RecordEntry { Duration = 1001 },
                },
            };

            this.sut.ShouldHaveValidationErrorFor(record => record.PaidLeaveType, record);
        }

        [TestCase(PaidLeaveType.Vacation)]
        [TestCase(PaidLeaveType.Sickness)]
        [TestCase(PaidLeaveType.MilitaryService)]
        [TestCase(PaidLeaveType.Other)]
        public void PaidLeaveType_IsFineWhenNotDoingOvertime(PaidLeaveType type)
        {
            var record = new Record
            {
                PaidLeaveType = type,
                NominalWorkTime = 1000,
                Entries = new List<RecordEntry>
                {
                    new RecordEntry { Duration = 1000 },
                },
            };

            this.sut.ShouldNotHaveValidationErrorFor(record => record.PaidLeaveType, record);
        }

        [Test]
        public void PaidLeaveType_IsFineWhenNull()
        {
            var record = new Record
            {
                PaidLeaveType = null,
                NominalWorkTime = 1000,
                Entries = new List<RecordEntry>
                {
                    new RecordEntry { Duration = 1001 },
                },
            };

            this.sut.ShouldNotHaveValidationErrorFor(record => record.PaidLeaveType, record);
        }
    }
}
