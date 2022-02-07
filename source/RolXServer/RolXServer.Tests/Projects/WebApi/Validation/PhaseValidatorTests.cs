// -----------------------------------------------------------------------
// <copyright file="PhaseValidatorTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Projects.WebApi.Resource;

namespace RolXServer.Projects.WebApi.Validation;

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
        var model = new Phase
        {
            Number = 0,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(phase => phase.Number);
    }

    [Test]
    public void Number_MustNotBeNegative()
    {
        var model = new Phase
        {
            Number = -42,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(phase => phase.Number);
    }

    [Test]
    public void Number_ShouldBePositive()
    {
        var model = new Phase
        {
            Number = 42,
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(phase => phase.Number);
    }

    [Test]
    public void Name_MustNotBeNull()
    {
        var model = new Phase
        {
            Name = null!,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(phase => phase.Name);
    }

    [Test]
    public void Name_MustNotBeEmpty()
    {
        var model = new Phase
        {
            Name = string.Empty,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(phase => phase.Name);
    }

    [Test]
    public void Name_MayBeAnyText()
    {
        var model = new Phase
        {
            Name = "The foo is in the bar!",
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(phase => phase.Name);
    }

    [Test]
    public void StartDate_MustNotBeNull()
    {
        var model = new Phase
        {
            StartDate = null!,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(phase => phase.StartDate);
    }

    [Test]
    public void StartDate_MustNotBeEmpty()
    {
        var model = new Phase
        {
            StartDate = string.Empty,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(phase => phase.StartDate);
    }

    [Test]
    public void StartDate_ShouldBeAnIsoFormattedDate()
    {
        var model = new Phase
        {
            StartDate = "2019-11-25",
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(phase => phase.StartDate);
    }

    [Test]
    public void StartDate_MustBeAValidDate()
    {
        var model = new Phase
        {
            StartDate = "2019-11-31",
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(phase => phase.StartDate);
    }

    [Test]
    public void EndDate_MayBeNull()
    {
        var model = new Phase
        {
            EndDate = null,
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(phase => phase.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MustNotBeEmpty()
    {
        var model = new Phase
        {
            EndDate = string.Empty,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(phase => phase.EndDate);
    }

    [Test]
    public void StartDate_IfNotNull_ShouldBeAnIsoFormattedDate()
    {
        var model = new Phase
        {
            EndDate = "2019-11-25",
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(phase => phase.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MustNotBeBeforeStartDate()
    {
        var model = new Phase
        {
            StartDate = "2019-11-25",
            EndDate = "2019-11-24",
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(phase => phase.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MayBeSameAsStartDate()
    {
        var model = new Phase
        {
            StartDate = "2019-11-25",
            EndDate = "2019-11-25",
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(phase => phase.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MayBeAfterStartDate()
    {
        var model = new Phase
        {
            StartDate = "2019-11-25",
            EndDate = "2019-11-26",
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(phase => phase.EndDate);
    }

    [Test]
    public void Budget_MustNotBeNegative()
    {
        var model = new Phase
        {
            Budget = -42,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(phase => phase.Budget);
    }

    [Test]
    public void Budget_ShouldBePositive()
    {
        var model = new Phase
        {
            Budget = 42,
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(phase => phase.Budget);
    }

    [Test]
    public void Budget_MayBeZero()
    {
        var model = new Phase
        {
            Budget = 0,
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(phase => phase.Budget);
    }
}
