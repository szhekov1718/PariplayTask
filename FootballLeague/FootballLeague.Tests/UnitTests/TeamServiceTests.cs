using FootballLeague.DAL;
using FootballLeague.Services.Contracts;
using FootballLeague.Services.DTOs.RequestDTOs;
using FootballLeague.Services.Exceptions;
using FootballLeague.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Tests.UnitTests
{
    [TestClass]
    public class TeamServiceTests
    {
        private Mock<ILeagueRankingService> _mockLeagueRankingService;
        private static DbContextOptions<FootballLeagueContext> _options;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Setup()
        {
            _mockLeagueRankingService = new Mock<ILeagueRankingService>();
            _options = Utils.GetOptions(TestContext.TestName);
        }

        [TestMethod]
        public async Task ShouldCreateTeam()
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
                var sut = new TeamService(assertContext, _mockLeagueRankingService.Object);

                var model = new TeamRequestDTO()
                {
                    Name = "Barcelona",
                    LeagueRankingId = 1
                };

                var result = await sut.CreateTeamAsync(model);

                Assert.AreEqual(model.Name, result.Name);
                Assert.AreEqual(model.LeagueRankingId, result.LeagueRankingId);
                Assert.AreEqual(7, assertContext.Teams.Count());
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenTeamExists()
        {
            var teams = Utils.SeedTeams();
            var leagueRankings = Utils.SeedLeagueRankings();

            using (var arrContext = new FootballLeagueContext(_options))
            {
                await arrContext.AddRangeAsync(teams);
                await arrContext.AddRangeAsync(leagueRankings);
                await arrContext.SaveChangesAsync();
            }

            var modelWithExistingName = new TeamRequestDTO()
            {
                Name = "Chelsea",
                LeagueRankingId = 1
            };

            using (var assertContext = new FootballLeagueContext(_options))
            {
                var sut = new TeamService(assertContext, _mockLeagueRankingService.Object);

                await Assert.ThrowsExceptionAsync<EntityAlreadyExistsException>(async () => await sut.CreateTeamAsync(modelWithExistingName));
            }
        }

        [TestMethod]
        public async Task ShouldDeleteTeam()
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
                var sut = new TeamService(assertContext, _mockLeagueRankingService.Object);

                await sut.DeleteTeamAsync(4);

                Assert.AreEqual(true, assertContext.Teams.First(x => x.Id == 4).IsDeleted);
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenTeamIsInvalid()
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
                var sut = new TeamService(assertContext, _mockLeagueRankingService.Object);

                await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await sut.DeleteTeamAsync(50));

                Assert.AreEqual(6, assertContext.Teams.Count());
            }
        }

        [TestMethod]
        public async Task ShouldGetTeamById()
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
                var sut = new TeamService(assertContext, _mockLeagueRankingService.Object);

                var result = await sut.GetTeamByIdAsync(2);

                var seedResult = teams.First(t => t.Id == result.Id);

                Assert.AreEqual("Chelsea", result.Name);
                Assert.AreEqual(2, result.Id);
                Assert.AreEqual(13, result.Points);
                Assert.AreEqual(1, result.LeagueRankingId);
            }
        }

        [TestMethod]
        public async Task ShouldValidateTeamExistsInLeagueRanking()
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
                var sut = new TeamService(assertContext, _mockLeagueRankingService.Object);

                await sut.TeamExistsValidator(1, 1);

                Assert.AreEqual(1, assertContext.Teams.FirstOrDefault().Id);
                Assert.AreEqual(1, assertContext.Teams.FirstOrDefault().LeagueRankingId);
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenTeamDoesNotExist()
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
                var sut = new TeamService(assertContext, _mockLeagueRankingService.Object);

                await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await sut.TeamExistsValidator(50, 2));
            }
        }
    }
}