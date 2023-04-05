using FootballLeague.DAL;
using FootballLeague.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace FootballLeague.Tests
{
    public class Utils
    {
        public static DbContextOptions<FootballLeagueContext> GetOptions(string databaseName)
        {
            var provider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

            return new DbContextOptionsBuilder<FootballLeagueContext>()
            .UseInMemoryDatabase(databaseName)
            .UseInternalServiceProvider(provider)
            .Options;
        }

        public static IEnumerable<Team> SeedTeams()
        {
            return new List<Team>()
            {
                 new Team()
                {
                    Id = 1,
                    Name = "Manchester United",
                    LeagueRankingId = 1,
                    Points = 15
                },
                new Team()
                {
                    Id = 2,
                    Name = "Chelsea",
                    LeagueRankingId = 1,
                    Points = 13
                },
                new Team()
                {
                    Id = 3,
                    Name = "Manchester City",
                    LeagueRankingId = 1,
                    Points = 8
                },
                new Team()
                {
                    Id = 4,
                    Name = "Levski",
                    LeagueRankingId = 2,
                    Points = 11
                },
                new Team()
                {
                    Id = 5,
                    Name = "Botev",
                    LeagueRankingId = 2,
                    Points = 6
                },
                 new Team()
                {
                    Id = 6,
                    Name = "CSKA",
                    LeagueRankingId = 2,
                    Points = 6
                }
            };
        }

        public static IEnumerable<Match> SeedMatches()
        {
            return new List<Match>()
            {
                 new Match()
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    HomeTeamScore = 4,
                    AwayTeamScore = 2,
                    IsPlayed = true,
                    LeagueRankingId = 1
                },
                new Match()
                {
                    Id = 2,
                    HomeTeamId = 2,
                    AwayTeamId = 3,
                    HomeTeamScore = 1,
                    AwayTeamScore = 0,
                    IsPlayed = true,
                    LeagueRankingId = 1
                },
                new Match()
                {
                    Id = 3,
                    HomeTeamId = 1,
                    AwayTeamId = 3,
                    HomeTeamScore = 2,
                    AwayTeamScore = 0,
                    IsPlayed = true,
                    LeagueRankingId = 1
                },
                new Match()
                {
                    Id = 4,
                    HomeTeamId = 4,
                    AwayTeamId = 5,
                    HomeTeamScore = 1,
                    AwayTeamScore = 1,
                    IsPlayed = true,
                    LeagueRankingId = 2
                },
                new Match()
                {
                    Id = 5,
                    HomeTeamId = 5,
                    AwayTeamId = 6,
                    HomeTeamScore = 4,
                    AwayTeamScore = 3,
                    IsPlayed = true,
                    LeagueRankingId = 2
                },
                new Match()
                {
                    Id = 6,
                    HomeTeamId = 6,
                    AwayTeamId = 4,
                    HomeTeamScore = 0,
                    AwayTeamScore = 2,
                    IsPlayed = true,
                    LeagueRankingId = 2
                },
            };
        }

        public static IEnumerable<LeagueRanking> SeedLeagueRankings()
        {
            return new List<LeagueRanking>()
            {
                new LeagueRanking()
                {
                    Id = 1,
                    Name = "Barclays Premier League"
                },
                new LeagueRanking()
                {
                    Id = 2,
                    Name = "Bulgarska Liga"
                }
            };
        }
    }
}
