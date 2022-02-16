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
    /// Gets or sets the favourite activities.
    /// </summary>
    public DbSet<FavouriteActivity> FavouriteActivities { get; set; } = null!;

    /// <summary>
    /// Gets or sets the activities.
    /// </summary>
    public DbSet<Activity> Activities { get; set; } = null!;

    /// <summary>
    /// Gets or sets the subprojects.
    /// </summary>
    public DbSet<Subproject> Subprojects { get; set; } = null!;

    /// <summary>
    /// Gets or sets the records.
    /// </summary>
    public DbSet<Record> Records { get; set; } = null!;

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

    /// <summary>
    /// Override this method to further configure the model that was discovered by convention from the entity types
    /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
    /// and re-used for subsequent instances of your derived context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
    /// define extension methods on this object that allow you to configure aspects of the model that are specific
    /// to a given database.</param>
    /// <remarks>
    /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
    /// then this method will not be run.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavouriteActivity>()
            .HasKey(s => new { s.UserId, s.ActivityId });

        modelBuilder.Entity<Record>()
            .HasIndex(r => new { r.Date, r.UserId })
            .IsUnique();

        modelBuilder.Entity<RecordEntry>()
            .ToTable("RecordEntries");

        modelBuilder.Entity<Activity>()
            .HasIndex(ph => new { ph.SubprojectId, ph.Number })
            .IsUnique();

        modelBuilder.Entity<Subproject>()
            .HasIndex(c => c.Number).IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.GoogleId).IsUnique();

        modelBuilder.Entity<UserPartTimeSetting>()
            .HasIndex(s => new { s.UserId, s.StartDate }).IsUnique();

        SeedSubprojects(modelBuilder);
    }

    private static void SeedSubprojects(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subproject>().HasData(new Subproject
        {
            Id = 1,
            Number = "P0001",
            Name = "Lockheed Martin",
        });

        modelBuilder.Entity<Activity>().HasData(new Activity
        {
            Id = 1,
            SubprojectId = 1,
            Number = 1,
            Name = "F35",
            FullName = "P0001.001 - Lockheed Martin - F35",
            StartDate = new DateTime(2021, 8, 22),
            IsBillable = true,
        });

        modelBuilder.Entity<Activity>().HasData(new Activity
        {
            Id = 2,
            SubprojectId = 1,
            Number = 2,
            Name = "F117-A",
            FullName = "P0001.002 - Lockheed Martin - F117-A",
            StartDate = new DateTime(2022, 2, 16),
            IsBillable = false,
        });

        modelBuilder.Entity<Activity>().HasData(new Activity
        {
            Id = 3,
            SubprojectId = 1,
            Number = 3,
            Name = "HaGaHuWa",
            FullName = "P0001.003 - Lockheed Martin - HaGaHuWa",
            StartDate = new DateTime(2021, 1, 1),
            EndDate = new DateTime(2022, 2, 16),
            IsBillable = true,
        });

        modelBuilder.Entity<Subproject>().HasData(new Subproject
        {
            Id = 2,
            Number = "P0002",
            Name = "SRF",
        });

        modelBuilder.Entity<Activity>().HasData(new Activity
        {
            Id = 4,
            SubprojectId = 2,
            Number = 1,
            Name = "Malony",
            FullName = "P0002.001 - SRF - Malony",
            StartDate = new DateTime(2021, 12, 5),
            IsBillable = true,
        });
    }
}
