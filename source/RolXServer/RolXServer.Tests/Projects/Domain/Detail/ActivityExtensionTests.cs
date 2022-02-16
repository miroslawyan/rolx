// -----------------------------------------------------------------------
// <copyright file="ActivityExtensionTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain.Detail;

public sealed class ActivityExtensionTests
{
    private Subproject subproject = null!;

    [SetUp]
    public void SetUp()
    {
        this.subproject = new Subproject
        {
            Number = "P1234",
            Name = "Foo",
            Activities = new List<Activity>
                {
                    new Activity
                    {
                        Number = 42,
                        Name = "Bar",
                    },
                },
        };

        foreach (var activity in this.subproject.Activities)
        {
            activity.Subproject = this.subproject;
        }
    }

    [Test]
    public void Sanitize_FullName()
    {
        var activity = this.subproject.Activities[0];
        activity.Sanitize();

        activity.FullName.Should().Be("P1234.042 - Foo - Bar");
    }

    [Test]
    public void Sanitize_Budget()
    {
        var activity = this.subproject.Activities[0];
        activity.Budget = TimeSpan.FromMinutes(0.5);
        activity.Sanitize();

        activity.Budget.Should().BeNull();
    }
}
