// -----------------------------------------------------------------------
// <copyright file="SubprojectServiceTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain.Detail;

public sealed class SubprojectServiceTests
{
    private static Subproject SeedSubproject => new Subproject
    {
        Id = 1,
        Number = 1,
        Name = "One",
        Activities = new List<Activity>
            {
                new Activity
                {
                    Number = 1,
                    Name = "One",
                },
                new Activity
                {
                    Number = 2,
                    Name = "Two",
                },
            },
    };

    [Test]
    public async Task Update_ExistingActivityChanged()
    {
        var subproject = SeedSubproject;
        var contextFactory = InMemory.ContextFactory(subproject);

        using (var context = contextFactory())
        {
            var sut = new SubprojectService(context);

            subproject.Activities[0].Name = "Changed";
            await sut.Update(subproject);
        }

        using (var context = contextFactory())
        {
            context.Subprojects
                .Include(s => s.Activities)
                .Single(s => s.Id == 1)
                .Activities.Single(a => a.Number == 1)
                .Name.Should().Be("Changed");
        }
    }

    [Test]
    public async Task Update_ExistingActivityRemoved()
    {
        var subproject = SeedSubproject;
        var contextFactory = InMemory.ContextFactory(subproject);

        using (var context = contextFactory())
        {
            var sut = new SubprojectService(context);

            subproject.Activities.RemoveAt(0);
            await sut.Update(subproject);
        }

        using (var context = contextFactory())
        {
            context.Subprojects
                .Include(s => s.Activities)
                .Single(s => s.Id == 1)
                .Activities.Count.Should().Be(1);
        }
    }

    [Test]
    public async Task Update_NewActivityAdded()
    {
        var subproject = SeedSubproject;
        var contextFactory = InMemory.ContextFactory(subproject);

        using (var context = contextFactory())
        {
            var sut = new SubprojectService(context);

            subproject.Activities.Add(new Activity
            {
                Number = 3,
                Name = "Three",
                Subproject = subproject,
            });
            await sut.Update(subproject);
        }

        using (var context = contextFactory())
        {
            context.Subprojects
                .Include(s => s.Activities)
                .Single(s => s.Id == 1)
                .Activities.Count.Should().Be(3);
        }
    }
}
