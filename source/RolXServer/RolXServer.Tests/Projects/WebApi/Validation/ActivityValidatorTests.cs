// -----------------------------------------------------------------------
// <copyright file="ActivityValidatorTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Projects.WebApi.Resource;

namespace RolXServer.Projects.WebApi.Validation;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public sealed class ActivityValidatorTests
{
    private readonly ActivityValidator sut = new ActivityValidator();
    private readonly Activity model = new();

    [Test]
    public void Number_MustBeGreaterThanZero()
    {
        this.model.Number = 0;
        this.sut.TestValidate(this.model).ShouldHaveValidationErrorFor(activity => activity.Number);
    }

    [Test]
    public void Number_MustBeLessThan100()
    {
        this.model.Number = 100;
        this.sut.TestValidate(this.model).ShouldHaveValidationErrorFor(activity => activity.Number);
    }

    [Test]
    public void Number_FineWhenWithinRange()
    {
        this.model.Number = 42;
        this.sut.TestValidate(this.model).ShouldNotHaveValidationErrorFor(activity => activity.Number);
    }

    [Test]
    public void Name_MustNotBeNull()
    {
        this.model.Name = null!;
        this.sut.TestValidate(this.model).ShouldHaveValidationErrorFor(activity => activity.Name);
    }

    [Test]
    public void Name_MustNotBeEmpty()
    {
        this.model.Name = string.Empty;
        this.sut.TestValidate(this.model).ShouldHaveValidationErrorFor(activity => activity.Name);
    }

    [Test]
    public void Name_MayBeAnyText()
    {
        this.model.Name = "The foo is in the bar!";
        this.sut.TestValidate(this.model).ShouldNotHaveValidationErrorFor(activity => activity.Name);
    }

    [Test]
    public void StartDate_MustNotBeNull()
    {
        this.model.StartDate = null!;
        this.sut.TestValidate(this.model).ShouldHaveValidationErrorFor(activity => activity.StartDate);
    }

    [Test]
    public void StartDate_MustNotBeEmpty()
    {
        this.model.StartDate = string.Empty;
        this.sut.TestValidate(this.model).ShouldHaveValidationErrorFor(activity => activity.StartDate);
    }

    [Test]
    public void StartDate_ShouldBeAnIsoFormattedDate()
    {
        this.model.StartDate = "2019-11-25";
        this.sut.TestValidate(this.model).ShouldNotHaveValidationErrorFor(activity => activity.StartDate);
    }

    [Test]
    public void StartDate_MustBeAValidDate()
    {
        this.model.StartDate = "2019-11-31";
        this.sut.TestValidate(this.model).ShouldHaveValidationErrorFor(activity => activity.StartDate);
    }

    [Test]
    public void EndDate_MayBeNull()
    {
        this.model.EndDate = null;
        this.sut.TestValidate(this.model).ShouldNotHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MustNotBeEmpty()
    {
        this.model.EndDate = string.Empty;
        this.sut.TestValidate(this.model).ShouldHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void StartDate_IfNotNull_ShouldBeAnIsoFormattedDate()
    {
        this.model.EndDate = "2019-11-25";
        this.sut.TestValidate(this.model).ShouldNotHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MustNotBeBeforeStartDate()
    {
        this.model.StartDate = "2019-11-25";
        this.model.EndDate = "2019-11-24";
        this.sut.TestValidate(this.model).ShouldHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MayBeSameAsStartDate()
    {
        this.model.StartDate = "2019-11-25";
        this.model.EndDate = "2019-11-25";
        this.sut.TestValidate(this.model).ShouldNotHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MayBeAfterStartDate()
    {
        this.model.StartDate = "2019-11-25";
        this.model.EndDate = "2019-11-26";
        this.sut.TestValidate(this.model).ShouldNotHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void Budget_MustNotBeNegative()
    {
        this.model.Budget = -42;
        this.sut.TestValidate(this.model).ShouldHaveValidationErrorFor(activity => activity.Budget);
    }

    [Test]
    public void Budget_ShouldBePositive()
    {
        this.model.Budget = 42;
        this.sut.TestValidate(this.model).ShouldNotHaveValidationErrorFor(activity => activity.Budget);
    }

    [Test]
    public void Budget_MayBeZero()
    {
        this.model.Budget = 0;
        this.sut.TestValidate(this.model).ShouldNotHaveValidationErrorFor(activity => activity.Budget);
    }
}
