﻿// <auto-generated />
using System;
using FootballLeague.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FootballLeague.DAL.Migrations
{
    [DbContext(typeof(FootballLeagueContext))]
    [Migration("20230405012442_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FootballLeague.DAL.Models.LeagueRanking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("LeagueRankings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Barclays Premier League"
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Bulgarska Liga"
                        });
                });

            modelBuilder.Entity("FootballLeague.DAL.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AwayTeamId")
                        .HasColumnType("int");

                    b.Property<int>("AwayTeamScore")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("HomeTeamId")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamScore")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPlayed")
                        .HasColumnType("bit");

                    b.Property<int>("LeagueRankingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("LeagueRankingId");

                    b.ToTable("Matches");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AwayTeamId = 2,
                            AwayTeamScore = 2,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeTeamId = 1,
                            HomeTeamScore = 4,
                            IsDeleted = false,
                            IsPlayed = true,
                            LeagueRankingId = 1
                        },
                        new
                        {
                            Id = 2,
                            AwayTeamId = 3,
                            AwayTeamScore = 0,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeTeamId = 2,
                            HomeTeamScore = 1,
                            IsDeleted = false,
                            IsPlayed = true,
                            LeagueRankingId = 1
                        },
                        new
                        {
                            Id = 3,
                            AwayTeamId = 3,
                            AwayTeamScore = 0,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeTeamId = 1,
                            HomeTeamScore = 2,
                            IsDeleted = false,
                            IsPlayed = true,
                            LeagueRankingId = 1
                        },
                        new
                        {
                            Id = 4,
                            AwayTeamId = 5,
                            AwayTeamScore = 1,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeTeamId = 4,
                            HomeTeamScore = 1,
                            IsDeleted = false,
                            IsPlayed = true,
                            LeagueRankingId = 2
                        },
                        new
                        {
                            Id = 5,
                            AwayTeamId = 6,
                            AwayTeamScore = 3,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeTeamId = 5,
                            HomeTeamScore = 4,
                            IsDeleted = false,
                            IsPlayed = true,
                            LeagueRankingId = 2
                        },
                        new
                        {
                            Id = 6,
                            AwayTeamId = 4,
                            AwayTeamScore = 2,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeTeamId = 6,
                            HomeTeamScore = 0,
                            IsDeleted = false,
                            IsPlayed = true,
                            LeagueRankingId = 2
                        });
                });

            modelBuilder.Entity("FootballLeague.DAL.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LeagueRankingId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LeagueRankingId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            LeagueRankingId = 1,
                            Name = "Manchester United",
                            Points = 15
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            LeagueRankingId = 1,
                            Name = "Chelsea",
                            Points = 13
                        },
                        new
                        {
                            Id = 3,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            LeagueRankingId = 1,
                            Name = "Manchester City",
                            Points = 8
                        },
                        new
                        {
                            Id = 4,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            LeagueRankingId = 2,
                            Name = "Levski",
                            Points = 11
                        },
                        new
                        {
                            Id = 5,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            LeagueRankingId = 2,
                            Name = "Botev",
                            Points = 6
                        },
                        new
                        {
                            Id = 6,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            LeagueRankingId = 2,
                            Name = "CSKA",
                            Points = 6
                        });
                });

            modelBuilder.Entity("FootballLeague.DAL.Models.Match", b =>
                {
                    b.HasOne("FootballLeague.DAL.Models.Team", "AwayTeam")
                        .WithMany("AwayMatchesPlayed")
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FootballLeague.DAL.Models.Team", "HomeTeam")
                        .WithMany("HomeMatchesPlayed")
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FootballLeague.DAL.Models.LeagueRanking", "LeagueRanking")
                        .WithMany("Matches")
                        .HasForeignKey("LeagueRankingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AwayTeam");

                    b.Navigation("HomeTeam");

                    b.Navigation("LeagueRanking");
                });

            modelBuilder.Entity("FootballLeague.DAL.Models.Team", b =>
                {
                    b.HasOne("FootballLeague.DAL.Models.LeagueRanking", "LeagueRanking")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueRankingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeagueRanking");
                });

            modelBuilder.Entity("FootballLeague.DAL.Models.LeagueRanking", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("FootballLeague.DAL.Models.Team", b =>
                {
                    b.Navigation("AwayMatchesPlayed");

                    b.Navigation("HomeMatchesPlayed");
                });
#pragma warning restore 612, 618
        }
    }
}
