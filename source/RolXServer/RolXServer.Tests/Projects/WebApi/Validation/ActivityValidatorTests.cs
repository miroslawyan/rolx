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
    private readonly Activity model = new(
            Id: 1,
            Number: 1,
            Name: "Name",
            StartDate: "2019-01-01",
            EndDate: null,
            BillabilityId: 1,
            BillabilityName: "Billable",
            IsBillable: true,
            Budget: 0,
            Actual: 0,
            FullNumber: "#0001.001.01",
            FullName: "Any - Full - Name",
            AllSubprojectNames: "Any - Full");

    [Test]
    public void Number_MustBeGreaterThanZero()
    {
        var model = this.model with { Number = 0 };
        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.Number);
    }

    [Test]
    public void Number_MustBeLessThan100()
    {
        var model = this.model with { Number = 100 };
        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.Number);
    }

    [Test]
    public void Number_FineWhenWithinRange()
    {
        var model = this.model with { Number = 42 };
        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.Number);
    }

    [Test]
    public void Name_MustNotBeNull()
    {
        var model = this.model with { Name = null! };
        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.Name);
    }

    [Test]
    public void Name_MustNotBeEmpty()
    {
        var model = this.model with { Name = string.Empty };
        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.Name);
    }

    [Test]
    public void Name_MayBeAnyText()
    {
        var model = this.model with { Name = "The foo is in the bar!" };
        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.Name);
    }

    [Test]
    public void StartDate_MustNotBeNull()
    {
        var model = this.model with { StartDate = null! };
        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.StartDate);
    }

    [Test]
    public void StartDate_MustNotBeEmpty()
    {
        var model = this.model with { StartDate = string.Empty };
        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.StartDate);
    }

    [Test]
    public void StartDate_ShouldBeAnIsoFormattedDate()
    {
        var model = this.model with { StartDate = "2019-11-25" };
        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.StartDate);
    }

    [Test]
    public void StartDate_MustBeAValidDate()
    {
        var model = this.model with { StartDate = "2019-11-31" };
        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.StartDate);
    }

    [Test]
    public void EndDate_MayBeNull()
    {
        var model = this.model with { EndDate = null };
        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MustNotBeEmpty()
    {
        var model = this.model with { EndDate = string.Empty };
        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void StartDate_IfNotNull_ShouldBeAnIsoFormattedDate()
    {
        var model = this.model with { EndDate = "2019-11-25" };
        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MustNotBeBeforeStartDate()
    {
        var model = this.model with { StartDate = "2019-11-25", EndDate = "2019-11-24" };
        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MayBeSameAsStartDate()
    {
        var model = this.model with { StartDate = "2019-11-25", EndDate = "2019-11-25" };
        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void EndDate_IfNotNull_MayBeAfterStartDate()
    {
        var model = this.model with { StartDate = "2019-11-25", EndDate = "2019-11-26" };
        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.EndDate);
    }

    [Test]
    public void Budget_MustNotBeNegative()
    {
        var model = this.model with { Budget = -42 };
        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(activity => activity.Budget);
    }

    [Test]
    public void Budget_ShouldBePositive()
    {
        var model = this.model with { Budget = 42 };
        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.Budget);
    }

    [Test]
    public void Budget_MayBeZero()
    {
        var model = this.model with { Budget = 0 };
        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(activity => activity.Budget);
    }
}
