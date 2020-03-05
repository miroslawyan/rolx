﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RolXServer;

namespace RolXServer.Migrations
{
    [DbContext(typeof(RolXContext))]
    [Migration("20200305205540_AddUserBalanceCorrections")]
    partial class AddUserBalanceCorrections
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("RolXServer.Projects.DataAccess.FavouritePhase", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("PhaseId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "PhaseId");

                    b.HasIndex("PhaseId");

                    b.ToTable("FavouritePhases");
                });

            modelBuilder.Entity("RolXServer.Projects.DataAccess.Phase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<TimeSpan?>("Budget")
                        .HasColumnType("interval");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsBillable")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId", "Number")
                        .IsUnique();

                    b.ToTable("Phases");
                });

            modelBuilder.Entity("RolXServer.Projects.DataAccess.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("RolXServer.Records.DataAccess.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("PaidLeaveReason")
                        .HasColumnType("text");

                    b.Property<int?>("PaidLeaveType")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("Date", "UserId")
                        .IsUnique();

                    b.ToTable("Records");
                });

            modelBuilder.Entity("RolXServer.Records.DataAccess.RecordEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<TimeSpan?>("Begin")
                        .HasColumnType("time");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("DurationSeconds")
                        .HasColumnType("bigint");

                    b.Property<TimeSpan?>("Pause")
                        .HasColumnType("interval");

                    b.Property<int>("PhaseId")
                        .HasColumnType("integer");

                    b.Property<int>("RecordId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PhaseId");

                    b.HasIndex("RecordId");

                    b.ToTable("RecordEntries");
                });

            modelBuilder.Entity("RolXServer.Users.DataAccess.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("EntryDate")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GoogleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("LeftDate")
                        .HasColumnType("date");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GoogleId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RolXServer.Users.DataAccess.UserBalanceCorrection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<TimeSpan>("Overtime")
                        .HasColumnType("interval");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<TimeSpan>("Vacation")
                        .HasColumnType("interval");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserBalanceCorrections");
                });

            modelBuilder.Entity("RolXServer.Users.DataAccess.UserPartTimeSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Factor")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "StartDate")
                        .IsUnique();

                    b.ToTable("UserPartTimeSettings");
                });

            modelBuilder.Entity("RolXServer.Projects.DataAccess.FavouritePhase", b =>
                {
                    b.HasOne("RolXServer.Projects.DataAccess.Phase", "Phase")
                        .WithMany()
                        .HasForeignKey("PhaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RolXServer.Users.DataAccess.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RolXServer.Projects.DataAccess.Phase", b =>
                {
                    b.HasOne("RolXServer.Projects.DataAccess.Project", "Project")
                        .WithMany("Phases")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RolXServer.Records.DataAccess.Record", b =>
                {
                    b.HasOne("RolXServer.Users.DataAccess.User", "User")
                        .WithMany("Records")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RolXServer.Records.DataAccess.RecordEntry", b =>
                {
                    b.HasOne("RolXServer.Projects.DataAccess.Phase", "Phase")
                        .WithMany()
                        .HasForeignKey("PhaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RolXServer.Records.DataAccess.Record", null)
                        .WithMany("Entries")
                        .HasForeignKey("RecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RolXServer.Users.DataAccess.UserBalanceCorrection", b =>
                {
                    b.HasOne("RolXServer.Users.DataAccess.User", null)
                        .WithMany("BalanceCorrections")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RolXServer.Users.DataAccess.UserPartTimeSetting", b =>
                {
                    b.HasOne("RolXServer.Users.DataAccess.User", null)
                        .WithMany("PartTimeSettings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
