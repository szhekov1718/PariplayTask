using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using FootballLeague.Services.Contracts;
using FootballLeague.Services.DTOs.RequestDTOs;
using FootballLeague.Services.DTOs.ResponseDTOs;

namespace FootballLeague.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeagueRankingController : ControllerBase
    {
        private readonly ILeagueRankingService _leagueRankingService;
        private readonly ILogger _logger;

        public LeagueRankingController(ILeagueRankingService leagueRankingService, ILogger<LeagueRankingController> logger)
        {
            _leagueRankingService = leagueRankingService;
            _logger = logger;
        }
      
        [HttpPost]
        public async Task<ActionResult> CreateLeagueRanking(LeagueRankingRequestDTO model)
        {
            _logger.LogInformation("Creating a league ranking.");

            var newLeagueRanking = await _leagueRankingService.CreateLeagueRankingAsync(model);

            return Ok(newLeagueRanking);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeagueRankingResponseDTO>> GetLeagueRankingScoreboard(int id)
        {
            _logger.LogInformation("Getting the league ranking scoreboard.");

            var leagueModel = await _leagueRankingService.GetLeagueRankingScoreboardAsync(id);

            return Ok(leagueModel);
        }
    }
}
