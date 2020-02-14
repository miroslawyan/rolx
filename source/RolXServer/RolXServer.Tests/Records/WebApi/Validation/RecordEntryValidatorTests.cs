// -----------------------------------------------------------------------
// <copyright file="RecordEntryValidatorTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using FluentValidation.TestHelper;
using NUnit.Framework;
using RolXServer.Projects.DataAccess;
using RolXServer.Records.WebApi.Resource;

namespace RolXServer.Records.WebApi.Validation
{
    /// <summary>
    /// Unit tests for the <see cref="RecordEntryValidator"/>.
    /// </summary>
    public sealed class RecordEntryValidatorTests
    {
        private RecordEntryValidator sut = null!;
        private Project project = null!;
        private RolXContext context = null!;
        private Record record = null!;

        private static Project SeedProject => new Project
        {
            Id = 1,
            Number = "1",
            Name = "One",
            Phases = new List<Phase>
            {
                new Phase
                {
                    Number = 1,
                    Name = "One",
                    StartDate = new DateTime(2019, 12, 17),
                },
                new Phase
                {
                    Number = 2,
                    Name = "Two",
                    StartDate = new DateTime(2019, 12, 17),
                    EndDate = new DateTime(2019, 12, 19),
                },
            },
        };

        [SetUp]
        public void SetUp()
        {
            this.project = SeedProject;
            this.context = InMemory.ContextFactory(this.project)();

            this.record = new Record
            {
                Date = "2019-12-18",
            };

            this.sut = new RecordEntryValidator(this.record, this.context);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }

        [Test]
        public void Duration_FailsWhenNegative()
        {
            this.sut.ShouldHaveValidationErrorFor(entry => entry.Duration, -12312312423432);
            this.sut.ShouldHaveValidationErrorFor(entry => entry.Duration, -42);
            this.sut.ShouldHaveValidationErrorFor(entry => entry.Duration, -1);
        }

        [Test]
        public void Duration_SucceedsWhenZero()
        {
            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.Duration, 0);
        }

        [Test]
        public void Duration_SucceedsWhenPositive()
        {
            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.Duration, 12312312423432);
            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.Duration, 42);
            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.Duration, 1);
        }

        [Test]
        public void PhaseId_FailsWhenZero()
        {
            this.sut.ShouldHaveValidationErrorFor(entry => entry.PhaseId, 0);
        }

        [Test]
        public void PhaseId_FailsWhenPhaseUnkownAndDurationNonZero()
        {
            var entry = new RecordEntry
            {
                PhaseId = 31415,
                Duration = 42,
            };

            this.sut.ShouldHaveValidationErrorFor(entry => entry.PhaseId, entry);
        }

        [Test]
        public void PhaseId_SucceedsWhenPhaseUnkownButDurationIsZero()
        {
            var entry = new RecordEntry
            {
                PhaseId = 31415,
                Duration = 0,
            };

            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.PhaseId, entry);
        }

        [Test]
        public void PhaseId_SucceedsWhenPhaseIsKnownAndOpen()
        {
            var entry = new RecordEntry
            {
                PhaseId = this.project.Phases[0].Id,
                Duration = 42,
            };

            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.PhaseId, entry);
        }

        [Test]
        public void PhaseId_SucceedsWhenPhaseOpensToday()
        {
            this.record.Date = "2019-12-17";

            var entry = new RecordEntry
            {
                PhaseId = this.project.Phases[1].Id,
                Duration = 42,
            };

            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.PhaseId, entry);
        }

        [Test]
        public void PhaseId_SucceedsWhenPhaseClosesToday()
        {
            this.record.Date = "2019-12-19";

            var entry = new RecordEntry
            {
                PhaseId = this.project.Phases[1].Id,
                Duration = 42,
            };

            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.PhaseId, entry);
        }

        [Test]
        public void PhaseId_FailsWhenPhaseClosedYesterday()
        {
            this.record.Date = "2019-12-20";

            var entry = new RecordEntry
            {
                PhaseId = this.project.Phases[1].Id,
                Duration = 42,
            };

            this.sut.ShouldHaveValidationErrorFor(entry => entry.PhaseId, entry);
        }

        [Test]
        public void PhaseId_FailsWhenPhaseOpensTomorrow()
        {
            this.record.Date = "2019-12-16";

            var entry = new RecordEntry
            {
                PhaseId = this.project.Phases[0].Id,
                Duration = 42,
            };

            this.sut.ShouldHaveValidationErrorFor(entry => entry.PhaseId, entry);
        }

        [Test]
        public void PhaseId_SucceedsWhenPhaseIsClosedButDurationIsZero()
        {
            this.record.Date = "2019-12-16";

            var entry = new RecordEntry
            {
                PhaseId = this.project.Phases[0].Id,
                Duration = 0,
            };

            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.PhaseId, entry);
        }

        [Test]
        public void Begin_SucceedsWhenNull()
        {
            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.Begin, (int?)null);
        }

        [Test]
        public void Begin_SucceedsWhenZero()
        {
            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.Begin, 0);
        }

        [Test]
        public void Begin_SucceedsWhenWithin24h()
        {
            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.Begin, 12 * 3600);
            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.Begin, 24 * 3600);
        }

        [Test]
        public void Begin_FailsWhenAbove24h()
        {
            this.sut.ShouldHaveValidationErrorFor(entry => entry.Begin, (24 * 3600) + 1);
            this.sut.ShouldHaveValidationErrorFor(entry => entry.Begin, (24 * 3600) + 123);
        }

        [Test]
        public void Begin_FailsWhenNegative()
        {
            this.sut.ShouldHaveValidationErrorFor(entry => entry.Begin, -1);
            this.sut.ShouldHaveValidationErrorFor(entry => entry.Begin, -11);
            this.sut.ShouldHaveValidationErrorFor(entry => entry.Begin, -111);
        }

        [Test]
        public void Pause_SucceedsWhenNull()
        {
            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.Pause, (int?)null);
        }

        [Test]
        public void Pause_FailsWhenNotNullButBeginIsNull()
        {
            var entry = new RecordEntry
            {
                Begin = null,
                Pause = 3600,
            };

            this.sut.ShouldHaveValidationErrorFor(entry => entry.Pause, entry);
        }

        [Test]
        public void Pause_SucceedsWhenZero()
        {
            var entry = new RecordEntry
            {
                Begin = 0,
                Pause = 0,
            };

            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.Pause, entry);
        }

        [Test]
        public void Pause_SucceedsWhenWithin24h()
        {
            var entry = new RecordEntry
            {
                Begin = 0,
                Pause = 12 * 3600,
            };

            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.Pause, entry);

            entry.Pause = 24 * 3600;
            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.Pause, entry);
        }

        [Test]
        public void Pause_FailsWhenNegative()
        {
            var entry = new RecordEntry
            {
                Begin = 0,
                Pause = -111,
            };

            this.sut.ShouldHaveValidationErrorFor(entry => entry.Pause, entry);
        }

        [Test]
        public void BeginPlusPausePlusDuration_SucceedsWhenWithin24h()
        {
            var entry = new RecordEntry
            {
                Duration = 11 * 3600,
                Begin = 12 * 3600,
                Pause = 1800,
            };

            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.Duration, entry);

            entry.Pause = 3600;
            this.sut.ShouldNotHaveValidationErrorFor(entry => entry.Duration, entry);
        }

        [Test]
        public void BeginPlusPausePlusDuration_FailsWhenAbove24h()
        {
            var entry = new RecordEntry
            {
                Duration = 11 * 3600,
                Begin = 12 * 3600,
                Pause = 3601,
            };

            this.sut.ShouldHaveValidationErrorFor(entry => entry.Duration, entry);
        }
    }
}
