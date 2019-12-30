// -----------------------------------------------------------------------
// <copyright file="RolXContext.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using RolXServer.Account.DataAccess;
using RolXServer.Auth.DataAccess;
using RolXServer.WorkRecord.DataAccess;

namespace RolXServer
{
    /// <summary>
    /// The database context in use.
    /// </summary>
    /// <seealso cref="DbContext" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "DI")]
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
        /// Gets or sets the favourite phases.
        /// </summary>
        public DbSet<FavouritePhase> FavouritePhases { get; set; } = null!;

        /// <summary>
        /// Gets or sets the phases.
        /// </summary>
        public DbSet<Phase> Phases { get; set; } = null!;

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        public DbSet<Project> Projects { get; set; } = null!;

        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        public DbSet<Record> Records { get; set; } = null!;

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public DbSet<User> Users { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user settings.
        /// </summary>
        public DbSet<UserSetting> UserSettings { get; set; } = null!;

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
            modelBuilder.Entity<FavouritePhase>()
                .HasKey(s => new { s.UserId, s.PhaseId });

            modelBuilder.Entity<Record>()
                .HasIndex(r => new { r.Date, r.UserId })
                .IsUnique();

            modelBuilder.Entity<RecordEntry>()
                .ToTable("RecordEntries");

            modelBuilder.Entity<Phase>()
                .HasIndex(ph => new { ph.ProjectId, ph.Number })
                .IsUnique();

            modelBuilder.Entity<Project>()
                .HasIndex(c => c.Number).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.GoogleId).IsUnique();

            modelBuilder.Entity<UserSetting>()
                .HasKey(s => new { s.UserId, s.StartDate });
        }
    }
}
