using FootballLeague.DAL;
using FootballLeague.Services.Contracts;
using FootballLeague.Services.DTOs.RequestDTOs;
using FootballLeague.Services.Exceptions;
using FootballLeague.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Tests.UnitTests
{
    [TestClass]
    public class LeagueRankingServiceTests
    {
        private static DbContextOptions<FootballLeagueContext> _options;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Setup()
        {
            _options = Utils.GetOptions(TestContext.TestName);
        }


        [TestMethod]
        public async Task ShouldCreateLeagueRanking()
        {
            var teams = Utils.SeedTeams();
            var leagueRankings = Utils.SeedLeagueRankings();

            using (var arrContext = new FootballLeagueContext(_options))
            {
                await arrContext.AddRangeAsync(teams);
                await arrContext.AddRangeAsync(leagueRankings);
                await arrContext.SaveChangesAsync();
            }

            using (var assertContext = new FootballLeagueContext(_options))
            {
                var sut = new LeagueRankingService(assertContext);

                var model = new LeagueRankingRequestDTO()
                {
                    Name = "Spanish La Liga"
                };

                var result = await sut.CreateLeagueRankingAsync(model);

                Assert.AreEqual(model.Name, result.Name);
                Assert.AreEqual(3, assertContext.LeagueRankings.Count());
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenLeagueRankingExists()
        {
            var teams = Utils.SeedTeams();
            var leagueRankings = Utils.SeedLeagueRankings();

            using (var arrContext = new FootballLeagueContext(_options))
            {
                await arrContext.AddRangeAsync(teams);
                await arrContext.AddRangeAsync(leagueRankings);
                await arrContext.SaveChangesAsync();
            }

            using (var assertContext = new FootballLeagueContext(_options))
            {
                var sut = new LeagueRankingService(assertContext);

                var existingLeagueRanking = new LeagueRankingRequestDTO()
                {
                    Name = "Bulgarska Liga"
                };

                await Assert.ThrowsExceptionAsync<EntityAlreadyExistsException>(async () => await sut.CreateLeagueRankingAsync(existingLeagueRanking));
            }
        }

        [TestMethod]
        public async Task ShouldCreateLeagueRankingScoreboard()
        {
            var teams = Utils.SeedTeams();
            var leagueRankings = Utils.SeedLeagueRankings();

            using (var arrContext = new FootballLeagueContext(_options))
            {
                await arrContext.AddRangeAsync(teams);
                await arrContext.AddRangeAsync(leagueRankings);
                await arrContext.SaveChangesAsync();
            }

            using (var assertContext = new FootballLeagueContext(_options))
            {
                var sut = new LeagueRankingService(assertContext);

                var scoreboard = await sut.GetLeagueRankingScoreboardAsync(2);

                Assert.IsNotNull(scoreboard);
                Assert.AreEqual(assertContext.LeagueRankings.FirstOrDefault(x => x.Id == 2).Name, scoreboard.Name);
                Assert.AreEqual(3, scoreboard.Teams.Count());
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionIfLeagueRankingDoesNotExist()
        {
            var teams = Utils.SeedTeams();
            var leagueRankings = Utils.SeedLeagueRankings();

            using (var arrContext = new FootballLeagueContext(_options))
            {
                await arrContext.AddRangeAsync(teams);
                await arrContext.AddRangeAsync(leagueRankings);
                await arrContext.SaveChangesAsync();
            }

            using (var assertContext = new FootballLeagueContext(_options))
            {
                var sut = new LeagueRankingService(assertContext);

                await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await sut.GetLeagueRankingScoreboardAsync(7));
            }
        }

        [TestMethod]
        public async Task ShouldValidateLeagueRankingExistsAsync()
        {
            var teams = Utils.SeedTeams();
            var leagueRankings = Utils.SeedLeagueRankings();

            using (var arrContext = new FootballLeagueContext(_options))
            {
                await arrContext.AddRangeAsync(teams);
                await arrContext.AddRangeAsync(leagueRankings);
                await arrContext.SaveChangesAsync();
            }

            using (var assertContext = new FootballLeagueContext(_options))
            {
                var sut = new LeagueRankingService(assertContext);

                await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await sut.LeagueRankingExistsValidator(7));
            }
        }
    }
}
