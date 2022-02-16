// -----------------------------------------------------------------------
// <copyright file="ActivityValidatorTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Projects.WebApi.Resource;

namespace RolXServer.Projects.WebApi.Validation;

public sealed class ActivityValidatorTests
{
    private ActivityValidator sut = null!;

    [SetUp]
    public void SetUp()
    {
        this.sut = new ActivityValidator();
    }

    [Test]
    public void Number_MustNotBeZero()
    {
        var model = new Activity
        {
            Number = 0,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.Number);
    }

    [Test]
    public void Number_MustNotBeNegative()
    {
        var model = new Activity
        {
            Number = -42,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.Number);
    }

    [Test]
    public void Number_ShouldBePositive()
    {
        var model = new Activity
        {
            Number = 42,
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.Number);
    }

    [Test]
    public void Name_MustNotBeNull()
    {
        var model = new Activity
        {
            Name = null!,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.Name);
    }

    [Test]
    public void Name_MustNotBeEmpty()
    {
        var model = new Activity
        {
            Name = string.Empty,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.Name);
    }

    [Test]
    public void Name_MayBeAnyText()
    {
        var model = new Activity
        {
            Name = "The foo is in the bar!",
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.Name);
    }

    [Test]
    public void StartDate_MustNotBeNull()
    {
        var model = new Activity
        {
            StartDate = null!,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.StartDate);
    }

    [Test]
    public void StartDate_MustNotBeEmpty()
    {
        var model = new Activity
        {
            StartDate = string.Empty,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.StartDate);
    }

    [Test]
    public void StartDate_ShouldBeAnIsoFormattedDate()
    {
        var model = new Activity
        {
            StartDate = "2019-11-25",
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.StartDate);
    }

    [Test]
    public void StartDate_MustBeAValidDate()
    {
        var model = new Activity
        {
            StartDate = "2019-11-31",
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.StartDate);
    }

    [Test]
    public void EndDate_MayBeNull()
    {
        var model = new Activity
        {
            EndDate = null,
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MustNotBeEmpty()
    {
        var model = new Activity
        {
            EndDate = string.Empty,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void StartDate_IfNotNull_ShouldBeAnIsoFormattedDate()
    {
        var model = new Activity
        {
            EndDate = "2019-11-25",
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MustNotBeBeforeStartDate()
    {
        var model = new Activity
        {
            StartDate = "2019-11-25",
            EndDate = "2019-11-24",
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MayBeSameAsStartDate()
    {
        var model = new Activity
        {
            StartDate = "2019-11-25",
            EndDate = "2019-11-25",
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MayBeAfterStartDate()
    {
        var model = new Activity
        {
            StartDate = "2019-11-25",
            EndDate = "2019-11-26",
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void Budget_MustNotBeNegative()
    {
        var model = new Activity
        {
            Budget = -42,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.Budget);
    }

    [Test]
    public void Budget_ShouldBePositive()
    {
        var model = new Activity
        {
            Budget = 42,
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.Budget);
    }

    [Test]
    public void Budget_MayBeZero()
    {
        var model = new Activity
        {
            Budget = 0,
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.Budget);
    }
}
