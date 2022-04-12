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
        configurationBuilder.Properties<DateOnly>().HavePrecision(0);
        configurationBuilder.Properties<TimeOnly>().HavePrecision(0);
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
            SortingWeight = 10,
        });
        modelBuilder.Entity<Billability>().HasData(new Billability
        {
            Id = 3,
            Name = "Verrechenbar TP",
            IsBillable = true,
            SortingWeight = 20,
        });
        modelBuilder.Entity<Billability>().HasData(new Billability
        {
            Id = 4,
            Name = "Verrechenbar Extern",
            IsBillable = true,
            SortingWeight = 30,
        });
        modelBuilder.Entity<Billability>().HasData(new Billability
        {
            Id = 5,
            Name = "Verrechenbar Nearshore",
            IsBillable = true,
            SortingWeight = 40,
        });
        modelBuilder.Entity<Billability>().HasData(new Billability
        {
            Id = 6,
            Name = "Verrechenbar 50+",
            IsBillable = true,
            SortingWeight = 50,
        });
        modelBuilder.Entity<Billability>().HasData(new Billability
        {
            Id = 7,
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
            Date = new DateOnly(2022, 1, 1),
        });
    }

    private static void SeedSubprojects(ModelBuilder modelBuilder)
    {
        var subproject = new Projects.Domain.Detail.PaidLeaveActivities().Subproject;
        var activities = subproject.Activities;
        subproject.Activities = new();

        modelBuilder.Entity<Subproject>().HasData(subproject);

        foreach (var activity in activities)
        {
            activity.Subproject = null;
            modelBuilder.Entity<Activity>().HasData(activity);
        }
    }
}
