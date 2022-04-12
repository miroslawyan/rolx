// -----------------------------------------------------------------------
// <copyright file="RecordServiceTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Moq;

using RolXServer.Projects.DataAccess;
using RolXServer.Users.DataAccess;

namespace RolXServer.Records.Domain.Detail;

public sealed class RecordServiceTests
{
    private static readonly DateOnly Today = DateOnly.FromDateTime(DateTime.Now);
    private static readonly DateOnly Tomorrow = Today.AddDays(1);

    private Func<RolXContext> contextFactory = null!;
    private User user = null!;
    private Activity activity = null!;

    [SetUp]
    public void SetUp()
    {
        this.user = new User
        {
            Id = Guid.NewGuid(),
        };

        this.activity = new Activity()
        {
            Number = 42,
        };

        var subproject = new Subproject
        {
            Activities = new List<Activity>
                {
                    this.activity,
                },
        };

        this.contextFactory = InMemory.ContextFactory(this.user, subproject);

        using (var context = this.contextFactory())
        {
            var todayRecord = new DataAccess.Record
            {
                UserId = this.user.Id,
                Date = Today,
                Entries = new List<DataAccess.RecordEntry>
                    {
                        new DataAccess.RecordEntry
                        {
                            ActivityId = this.activity.Id,
                            Duration = TimeSpan.FromHours(2.5),
                        },
                    },
            };

            context.Records.Add(todayRecord);
            context.SaveChanges();
        }
    }

    [Test]
    public async Task Update_NewRecord()
    {
        using (var context = this.contextFactory())
        {
            var sut = new RecordService(context, Mock.Of<IOptions<Settings>>());

            var record = new Model.Record(new Model.DayInfo { Date = Tomorrow })
            {
                UserId = this.user.Id,
                Entries = new List<DataAccess.RecordEntry>
                    {
                        new DataAccess.RecordEntry
                        {
                            ActivityId = this.activity.Id,
                            Duration = TimeSpan.FromHours(8.4),
                        },
                    },
            };

            await sut.Update(record);
        }

        using (var context = this.contextFactory())
        {
            var record = context.Records
                .Include(r => r.Entries)
                .Single(r => r.UserId == this.user.Id && r.Date == Tomorrow);

            record.Entries.Single().Duration.Should().Be(TimeSpan.FromHours(8.4));
        }
    }

    [Test]
    public async Task Update_ExistingRecord_MoreEntries()
    {
        using (var context = this.contextFactory())
        {
            var sut = new RecordService(context, Mock.Of<IOptions<Settings>>());

            var record = new Model.Record(new Model.DayInfo { Date = Today })
            {
                UserId = this.user.Id,
                Entries = new List<DataAccess.RecordEntry>
                    {
                        new DataAccess.RecordEntry
                        {
                            ActivityId = this.activity.Id,
                            Duration = TimeSpan.FromHours(3),
                        },
                        new DataAccess.RecordEntry
                        {
                            ActivityId = this.activity.Id,
                            Duration = TimeSpan.FromHours(1),
                        },
                    },
            };

            await sut.Update(record);
        }

        using (var context = this.contextFactory())
        {
            var record = context.Records
                .Include(r => r.Entries)
                .Single(r => r.UserId == this.user.Id && r.Date == Today);

            record.Entries.Should().HaveCount(2);
            record.Entries.Any(e => e.Duration == TimeSpan.FromHours(3)).Should().BeTrue();
            record.Entries.Any(e => e.Duration == TimeSpan.FromHours(1)).Should().BeTrue();
        }
    }

    [Test]
    public async Task Update_ExistingRecord_NoEntries()
    {
        using (var context = this.contextFactory())
        {
            var sut = new RecordService(context, Mock.Of<IOptions<Settings>>());

            var record = new Model.Record(new Model.DayInfo { Date = Today })
            {
                UserId = this.user.Id,
            };

            await sut.Update(record);
        }

        using (var context = this.contextFactory())
        {
            context.Records
                .Any(r => r.UserId == this.user.Id && r.Date == Today)
                .Should().BeFalse();
        }
    }
}
