using FootballLeague.DAL;
using FootballLeague.Services.Contracts;
using FootballLeague.Services.DTOs.RequestDTOs;
using FootballLeague.Services.Exceptions;
using FootballLeague.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Tests.UnitTests
{
    [TestClass]
    public class MatchServiceTests
    {
        private static DbContextOptions<FootballLeagueContext> _options;
        private Mock<ITeamService> _mockTeamService;
        private Mock<ILeagueRankingService> _mockLeagueRankingService;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Setup()
        {
            _mockTeamService = new Mock<ITeamService>();
            _mockLeagueRankingService = new Mock<ILeagueRankingService>();
            _options = Utils.GetOptions(TestContext.TestName);
        }

        [TestMethod]
        public async Task ShouldReturnCreatedMatch()
        {
            var teams = Utils.SeedTeams();
            var matches = Utils.SeedMatches();
            var leagueRankings = Utils.SeedLeagueRankings();

            using (var arrContext = new FootballLeagueContext(_options))
            {
                await arrContext.AddRangeAsync(teams);
                await arrContext.AddRangeAsync(matches);
                await arrContext.AddRangeAsync(leagueRankings);
                await arrContext.SaveChangesAsync();
            }

            var model = new MatchRequestDTO()
            {
                Id = 7,
                AwayTeamId = 2,
                AwayTeamScore = 5,
                HomeTeamId = 1,
                HomeTeamScore = 2,
                LeagueRankingId = 1
            };

            using (var assertContext = new FootballLeagueContext(_options))
            {
                var sut = new MatchService(assertContext, _mockLeagueRankingService.Object, _mockTeamService.Object);

                var result = await sut.CreateMatchAsync(model);

                Assert.AreEqual(model.Id, result.Id);
                Assert.AreEqual(model.AwayTeamId, result.AwayTeamId);
                Assert.AreEqual(model.AwayTeamScore, result.AwayTeamScore);
                Assert.AreEqual(model.HomeTeamScore, result.HomeTeamScore);
                Assert.AreEqual(model.HomeTeamId, result.HomeTeamId);
                Assert.AreEqual(7, assertContext.Matches.Count());
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenModelIsNull()
        {
            var teams = Utils.SeedTeams();
            var matches = Utils.SeedMatches();

            using (var arrContext = new FootballLeagueContext(_options))
            {
                await arrContext.AddRangeAsync(teams);
                await arrContext.AddRangeAsync(matches);
                await arrContext.SaveChangesAsync();
            }

            using (var assertContext = new FootballLeagueContext(_options))
            {
                var sut = new MatchService(assertContext, _mockLeagueRankingService.Object, _mockTeamService.Object);

                await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await sut.CreateMatchAsync(null));
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenIsNotFound()
        {
            var teams = Utils.SeedTeams();
            var matches = Utils.SeedMatches();

            using (var arrContext = new FootballLeagueContext(_options))
            {
                await arrContext.AddRangeAsync(teams);
                await arrContext.AddRangeAsync(matches);
                await arrContext.SaveChangesAsync();
            }

            using (var assertContext = new FootballLeagueContext(_options))
            {
                var sut = new MatchService(assertContext, _mockLeagueRankingService.Object, _mockTeamService.Object);

                await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await sut.DeleteMatchAsync(50));
            }
        }

        [TestMethod]
        public async Task ShouldGetAllMatches()
        {
            var teams = Utils.SeedTeams();
            var matches = Utils.SeedMatches();
            var leagueRankings = Utils.SeedLeagueRankings();

            using (var arrContext = new FootballLeagueContext(_options))
            {
                await arrContext.AddRangeAsync(teams);
                await arrContext.AddRangeAsync(matches);
                await arrContext.AddRangeAsync(leagueRankings);
                await arrContext.SaveChangesAsync();
            }

            using (var assertContext = new FootballLeagueContext(_options))
            {
                var sut = new MatchService(assertContext, _mockLeagueRankingService.Object, _mockTeamService.Object);

                var result = await sut.GetAllMatchesAsync();

                Assert.AreEqual(6, result.Count);
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenMatchHasAlreadyBeenPlayed()
        {
            var teams = Utils.SeedTeams();
            var matches = Utils.SeedMatches();
            var leagueRankings = Utils.SeedLeagueRankings();

            using (var arrContext = new FootballLeagueContext(_options))
            {
                await arrContext.AddRangeAsync(teams);
                await arrContext.AddRangeAsync(matches);
                await arrContext.AddRangeAsync(leagueRankings);
                await arrContext.SaveChangesAsync();
            }

            var model = new MatchRequestDTO()
            {
                Id = 1,
                HomeTeamId = 1,
                AwayTeamId = 2,
                HomeTeamScore = 4,
                AwayTeamScore = 2,
                LeagueRankingId = 1
            };

            using (var assertContext = new FootballLeagueContext(_options))
            {
                var sut = new MatchService(assertContext, _mockLeagueRankingService.Object, _mockTeamService.Object);

                await Assert.ThrowsExceptionAsync<MatchHasAlreadyBeenPlayedException>(async () => await sut.PlayMatchAsync(model));
            }
        }

        [TestMethod]
        public async Task ShouldGetAllMatchesByTeam()
        {
            var teams = Utils.SeedTeams();
            var matches = Utils.SeedMatches();
            var leagueRankings = Utils.SeedLeagueRankings();

            using (var arrContext = new FootballLeagueContext(_options))
            {
                await arrContext.AddRangeAsync(teams);
                await arrContext.AddRangeAsync(matches);
                await arrContext.AddRangeAsync(leagueRankings);
                await arrContext.SaveChangesAsync();
            }

            using (var assertContext = new FootballLeagueContext(_options))
            {
                var sut = new MatchService(assertContext, _mockLeagueRankingService.Object, _mockTeamService.Object);

                var result = await sut.GetAllMatchesByTeamAsync(1);

                Assert.AreEqual(2, result.Count);
            }
        }
    }
}
