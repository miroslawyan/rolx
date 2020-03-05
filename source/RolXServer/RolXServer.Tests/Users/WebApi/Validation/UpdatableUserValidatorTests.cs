// -----------------------------------------------------------------------
// <copyright file="UpdatableUserValidatorTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation.TestHelper;
using NUnit.Framework;
using RolXServer.Users.WebApi.Resource;

namespace RolXServer.Users.WebApi.Validation
{
    public sealed class UpdatableUserValidatorTests
    {
        private UpdatableUserValidator sut = null!;

        [SetUp]
        public void SetUp()
        {
            this.sut = new UpdatableUserValidator();
        }

        [Test]
        public void EntryDate_MayBeNull()
        {
            this.sut.ShouldNotHaveValidationErrorFor(user => user.EntryDate, (string?)null);
        }

        [Test]
        public void EntryDate_MustNotBeEmpty()
        {
            this.sut.ShouldHaveValidationErrorFor(user => user.EntryDate, string.Empty);
        }

        [Test]
        public void EntryDate_MustBeValidIsoDate()
        {
            this.sut.ShouldNotHaveValidationErrorFor(user => user.EntryDate, "2019-12-14");
            this.sut.ShouldHaveValidationErrorFor(user => user.EntryDate, "2019-13-14");
        }

        [Test]
        public void LeavingDate_MayBeNull()
        {
            this.sut.ShouldNotHaveValidationErrorFor(user => user.LeftDate, (string?)null);
        }

        [Test]
        public void LeavingDate_MustNotBeEmpty()
        {
            this.sut.ShouldHaveValidationErrorFor(user => user.LeftDate, string.Empty);
        }

        [Test]
        public void LeavingDate_Valid()
        {
            this.sut.ShouldNotHaveValidationErrorFor(user => user.LeftDate, new UpdatableUser
            {
                EntryDate = "2019-11-14",
                LeftDate = "2019-12-14",
            });
        }

        [Test]
        public void LeavingDate_MustBeNullWhileEntryDateIsNull()
        {
            this.sut.ShouldHaveValidationErrorFor(user => user.LeftDate, new UpdatableUser
            {
                EntryDate = null,
                LeftDate = "2019-12-14",
            });
        }

        [Test]
        public void LeavingDate_MustBeValidIsoDate()
        {
            this.sut.ShouldHaveValidationErrorFor(user => user.LeftDate, new UpdatableUser
            {
                EntryDate = "2019-12-14",
                LeftDate = "2019-13-14",
            });
        }

        [Test]
        public void LeavingDate_MustBeAfterEntryDate()
        {
            this.sut.ShouldHaveValidationErrorFor(user => user.LeftDate, new UpdatableUser
            {
                EntryDate = "2019-12-14",
                LeftDate = "2019-12-14",
            });
            this.sut.ShouldHaveValidationErrorFor(user => user.LeftDate, new UpdatableUser
            {
                EntryDate = "2019-12-14",
                LeftDate = "2019-12-13",
            });
        }
    }
}
