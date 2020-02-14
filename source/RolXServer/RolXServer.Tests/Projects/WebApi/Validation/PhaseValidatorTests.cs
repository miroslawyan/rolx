// -----------------------------------------------------------------------
// <copyright file="PhaseValidatorTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation.TestHelper;
using NUnit.Framework;
using RolXServer.Projects.WebApi.Resource;

namespace RolXServer.Projects.WebApi.Validation
{
    /// <summary>
    /// Unit tests for the <see cref="PhaseValidator"/>.
    /// </summary>
    public sealed class PhaseValidatorTests
    {
        private PhaseValidator sut = null!;

        [SetUp]
        public void SetUp()
        {
            this.sut = new PhaseValidator();
        }

        [Test]
        public void Number_MustNotBeZero()
        {
            this.sut.ShouldHaveValidationErrorFor(phase => phase.Number, 0);
        }

        [Test]
        public void Number_MustNotBeNegative()
        {
            this.sut.ShouldHaveValidationErrorFor(phase => phase.Number, -42);
        }

        [Test]
        public void Number_ShouldBePositive()
        {
            this.sut.ShouldNotHaveValidationErrorFor(phase => phase.Number, 42);
        }

        [Test]
        public void Name_MustNotBeNull()
        {
            this.sut.ShouldHaveValidationErrorFor(phase => phase.Name, null as string);
        }

        [Test]
        public void Name_MustNotBeEmpty()
        {
            this.sut.ShouldHaveValidationErrorFor(phase => phase.Name, string.Empty);
        }

        [Test]
        public void Name_MayBeAnyText()
        {
            this.sut.ShouldNotHaveValidationErrorFor(phase => phase.Name, "The foo is in the bar!");
        }

        [Test]
        public void StartDate_MustNotBeNull()
        {
            this.sut.ShouldHaveValidationErrorFor(phase => phase.StartDate, null as string);
        }

        [Test]
        public void StartDate_MustNotBeEmpty()
        {
            this.sut.ShouldHaveValidationErrorFor(phase => phase.StartDate, string.Empty);
        }

        [Test]
        public void StartDate_ShouldBeAnIsoFormattedDate()
        {
            this.sut.ShouldNotHaveValidationErrorFor(phase => phase.StartDate, "2019-11-25");
        }

        [Test]
        public void StartDate_MustBeAValidDate()
        {
            this.sut.ShouldHaveValidationErrorFor(phase => phase.StartDate, "2019-11-31");
        }

        [Test]
        public void EndDate_MayBeNull()
        {
            this.sut.ShouldNotHaveValidationErrorFor(phase => phase.EndDate, null as string);
        }

        [Test]
        public void EndDate_IfNotNull_MustNotBeEmpty()
        {
            this.sut.ShouldHaveValidationErrorFor(phase => phase.EndDate, string.Empty);
        }

        [Test]
        public void StartDate_IfNotNull_ShouldBeAnIsoFormattedDate()
        {
            this.sut.ShouldNotHaveValidationErrorFor(phase => phase.EndDate, "2019-11-25");
        }

        [Test]
        public void EndDate_IfNotNull_MustNotBeBeforeStartDate()
        {
            var phase = new Phase
            {
                StartDate = "2019-11-25",
                EndDate = "2019-11-24",
            };

            this.sut.ShouldHaveValidationErrorFor(phase => phase.EndDate, phase);
        }

        [Test]
        public void EndDate_IfNotNull_MayBeSameAsStartDate()
        {
            var phase = new Phase
            {
                StartDate = "2019-11-25",
                EndDate = "2019-11-25",
            };

            this.sut.ShouldNotHaveValidationErrorFor(phase => phase.EndDate, phase);
        }

        [Test]
        public void EndDate_IfNotNull_MayBeAfterStartDate()
        {
            var phase = new Phase
            {
                StartDate = "2019-11-25",
                EndDate = "2019-11-26",
            };

            this.sut.ShouldNotHaveValidationErrorFor(phase => phase.EndDate, phase);
        }

        [Test]
        public void Budget_IfNotNull_MustNotBeNegative()
        {
            this.sut.ShouldHaveValidationErrorFor(phase => phase.Budget, -42);
        }

        [Test]
        public void Budget_IfNotNull_ShouldBePositive()
        {
            this.sut.ShouldNotHaveValidationErrorFor(phase => phase.Budget, 42);
        }

        [Test]
        public void Budget_IfNotNull_MayBeZero()
        {
            this.sut.ShouldNotHaveValidationErrorFor(phase => phase.Budget, 0);
        }
    }
}
