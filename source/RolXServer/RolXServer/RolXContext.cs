// -----------------------------------------------------------------------
// <copyright file="RolXContext.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

using RolXServer.Projects.DataAccess;
using RolXServer.Records.DataAccess;
using RolXServer.Users.DataAccess;

namespace RolXServer;

/// <summary>
/// The database context in use.
/// </summary>
/// <seealso cref="DbContext" />
public sealed class RolXContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RolXContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public RolXContext(DbContextOptions<RolXContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the activities.
    /// </summary>
    public DbSet<Activity> Activities { get; set; } = null!;

    /// <summary>
    /// Gets or sets the billabilities.
    /// </summary>
    public DbSet<Billability> Billabilities { get; set; } = null!;

    /// <summary>
    /// Gets or sets the edit locks.
    /// </summary>
    public DbSet<EditLock> EditLocks { get; set; } = null!;

    /// <summary>
    /// Gets or sets the favourite activities.
    /// </summary>
    public DbSet<FavouriteActivity> FavouriteActivities { get; set; } = null!;

    /// <summary>
    /// Gets or sets the records.
    /// </summary>
    public DbSet<Record> Records { get; set; } = null!;

    /// <summary>
    /// Gets or sets the record entries.
    /// </summary>
    public DbSet<RecordEntry> RecordEntries { get; set; } = null!;

    /// <summary>
    /// Gets or sets the subprojects.
    /// </summary>
    public DbSet<Subproject> Subprojects { get; set; } = null!;

    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Gets or sets the balance corrections.
    /// </summary>
    public DbSet<UserBalanceCorrection> UserBalanceCorrections { get; set; } = null!;

    /// <summary>
    /// Gets or sets the part-time settings.
    /// </summary>
    public DbSet<UserPartTimeSetting> UserPartTimeSettings { get; set; } = null!;

    /// <inheritdoc/>
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HavePrecision(0);
        configurationBuilder.Properties<TimeSpan>().HavePrecision(0);
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>()
           .HasIndex(a => new { a.SubprojectId, a.Number })
           .IsUnique();

        modelBuilder.Entity<Activity>()
            .HasOne(e => e.Billability)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FavouriteActivity>()
            .HasKey(s => new { s.UserId, s.ActivityId });

        modelBuilder.Entity<Record>()
            .HasIndex(r => new { r.Date, r.UserId })
            .IsUnique();

        modelBuilder.Entity<RecordEntry>()
            .HasOne(e => e.Activity)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Subproject>()
             .HasIndex(s => new { s.ProjectNumber, s.Number }).IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.GoogleId).IsUnique();

        modelBuilder.Entity<UserBalanceCorrection>()
            .HasIndex(s => s.UserId);

        modelBuilder.Entity<UserBalanceCorrection>()
            .HasIndex(s => new { s.UserId, s.Date }).IsUnique();

        modelBuilder.Entity<UserPartTimeSetting>()
            .HasIndex(s => new { s.UserId, s.StartDate }).IsUnique();

        SeedBillabilities(modelBuilder);
        SeedEditLocks(modelBuilder);
        SeedSubprojects(modelBuilder);
    }

    private static void SeedBillabilities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Billability>().HasData(new Billability
        {
            Id = 1,
            Name = "Nicht verrechenbar",
            IsBillable = false,
            SortingWeight = 100,
        });
        modelBuilder.Entity<Billability>().HasData(new Billability
        {
            Id = 2,
            Name = "Verrechenbar Engineering",
            IsBillable = true,
            SortingWeight = 1,
        });
        modelBuilder.Entity<Billability>().HasData(new Billability
        {
            Id = 3,
            Name = "Verrechenbar TP",
            IsBillable = true,
            SortingWeight = 2,
        });
        modelBuilder.Entity<Billability>().HasData(new Billability
        {
            Id = 4,
            Name = "Verrechenbar 50+",
            IsBillable = true,
            SortingWeight = 3,
        });
        modelBuilder.Entity<Billability>().HasData(new Billability
        {
            Id = 5,
            Name = "Abwesenheit",
            IsBillable = false,
            SortingWeight = 200,
        });
    }

    private static void SeedEditLocks(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EditLock>().HasData(new EditLock
        {
            Id = RolXServer.Records.Domain.Detail.EditLockService.OneAndOnlyId,
            Date = new DateTime(2022, 1, 1),
        });
    }

    private static void SeedSubprojects(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subproject>().HasData(new Subproject
        {
            Id = 1,
            Number = 1,
            Name = "F35",
            ProjectNumber = 4711,
            ProjectName = "Auto Pilot",
            CustomerName = "Lockheed Martin",
        });

        modelBuilder.Entity<Activity>().HasData(new Activity
        {
            Id = 11,
            SubprojectId = 1,
            Number = 1,
            Name = "Take off",
            StartDate = new DateTime(2021, 8, 22),
            BillabilityId = 2,
        });

        modelBuilder.Entity<Activity>().HasData(new Activity
        {
            Id = 12,
            SubprojectId = 1,
            Number = 2,
            Name = "Cruise",
            StartDate = new DateTime(2022, 2, 16),
            BillabilityId = 1,
        });

        modelBuilder.Entity<Activity>().HasData(new Activity
        {
            Id = 13,
            SubprojectId = 1,
            Number = 3,
            Name = "Landing",
            StartDate = new DateTime(2022, 1, 1),
            EndDate = new DateTime(2022, 3, 16),
            BillabilityId = 3,
        });

        modelBuilder.Entity<Subproject>().HasData(new Subproject
        {
            Id = 2,
            Number = 2,
            Name = "F117A",
            ProjectNumber = 4711,
            ProjectName = "Auto Pilot",
            CustomerName = "Lockheed Martin",
        });

        modelBuilder.Entity<Activity>().HasData(new Activity
        {
            Id = 21,
            SubprojectId = 2,
            Number = 1,
            Name = "Take off",
            StartDate = new DateTime(2021, 8, 22),
            BillabilityId = 4,
        });

        modelBuilder.Entity<Activity>().HasData(new Activity
        {
            Id = 22,
            SubprojectId = 2,
            Number = 2,
            Name = "Cruise",
            StartDate = new DateTime(2022, 2, 16),
            BillabilityId = 1,
        });

        modelBuilder.Entity<Activity>().HasData(new Activity
        {
            Id = 23,
            SubprojectId = 2,
            Number = 3,
            Name = "Landing",
            StartDate = new DateTime(2022, 1, 1),
            EndDate = new DateTime(2022, 3, 16),
            BillabilityId = 2,
        });

        modelBuilder.Entity<Subproject>().HasData(new Subproject
        {
            Id = 3,
            Number = 1,
            Name = "Fragengenerator",
            ProjectNumber = 3141,
            ProjectName = "ABC SRF 3",
            CustomerName = "SRF",
        });

        modelBuilder.Entity<Activity>().HasData(new Activity
        {
            Id = 31,
            SubprojectId = 3,
            Number = 1,
            Name = "Analyse",
            StartDate = new DateTime(2022, 1, 1),
            BillabilityId = 3,
        });

        modelBuilder.Entity<Activity>().HasData(new Activity
        {
            Id = 32,
            SubprojectId = 3,
            Number = 2,
            Name = "Umsetzung",
            StartDate = new DateTime(2022, 1, 1),
            BillabilityId = 4,
        });

        modelBuilder.Entity<Activity>().HasData(new Activity
        {
            Id = 33,
            SubprojectId = 3,
            Number = 3,
            Name = "Übergabe",
            StartDate = new DateTime(2022, 1, 1),
            BillabilityId = 2,
        });
    }
}
