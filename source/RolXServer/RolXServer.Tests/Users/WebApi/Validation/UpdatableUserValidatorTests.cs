// -----------------------------------------------------------------------
// <copyright file="UpdatableUserValidatorTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Users.WebApi.Resource;

namespace RolXServer.Users.WebApi.Validation;

public sealed class UpdatableUserValidatorTests
{
    private UpdatableUserValidator sut = null!;

    [SetUp]
    public void SetUp()
    {
        this.sut = new UpdatableUserValidator();
    }

    [Test]
    public void EntryDate_MustNotBeEmpty()
    {
        var model = new UpdatableUser
        {
            EntryDate = string.Empty,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(record => record.EntryDate);
    }

    [Test]
    public void EntryDate_MustBeValidIsoDate()
    {
        var model = new UpdatableUser
        {
            EntryDate = "2019-12-14",
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(record => record.EntryDate);
    }

    [Test]
    public void EntryDate_MustNotBeInvalidIsoDate()
    {
        var model = new UpdatableUser
        {
            EntryDate = "2019-13-14",
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(record => record.EntryDate);
    }

    [Test]
    public void LeavingDate_MayBeNull()
    {
        var model = new UpdatableUser
        {
            LeavingDate = null,
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(record => record.LeavingDate);
    }

    [Test]
    public void LeavingDate_MustNotBeEmpty()
    {
        var model = new UpdatableUser
        {
            LeavingDate = string.Empty,
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(record => record.LeavingDate);
    }

    [Test]
    public void LeavingDate_Valid()
    {
        var model = new UpdatableUser
        {
            EntryDate = "2019-11-14",
            LeavingDate = "2019-12-14",
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(record => record.LeavingDate);
    }

    [Test]
    public void LeavingDate_MustBeValidIsoDate()
    {
        var model = new UpdatableUser
        {
            EntryDate = "2019-12-14",
            LeavingDate = "2019-13-14",
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(record => record.LeavingDate);
    }

    [Test]
    public void LeavingDate_MayBeSameAsEntryDate()
    {
        var model = new UpdatableUser
        {
            EntryDate = "2019-12-14",
            LeavingDate = "2019-12-14",
        };

        this.sut.TestValidate(model).ShouldNotHaveValidationErrorFor(record => record.LeavingDate);
    }

    [Test]
    public void LeavingDate_MustBeAfterEntryDate()
    {
        var model = new UpdatableUser
        {
            EntryDate = "2019-12-14",
            LeavingDate = "2019-12-13",
        };

        this.sut.TestValidate(model).ShouldHaveValidationErrorFor(record => record.LeavingDate);
    }
}
