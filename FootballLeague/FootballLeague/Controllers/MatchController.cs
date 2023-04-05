using FootballLeague.Services.Contracts;
using FootballLeague.Services.DTOs.RequestDTOs;
using FootballLeague.Services.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeague.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService, ILogger<MatchController> logger)
        {
            _matchService = matchService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> CreateMatch(MatchRequestDTO model)
        {
            _logger.LogInformation("Creating a match.");

            var newMatch = await _matchService.CreateMatchAsync(model);

            return Ok(newMatch);
        }

        [HttpPut]
        public async Task<ActionResult> PlayMatch(MatchRequestDTO model)
        {
            _logger.LogInformation("Playing a match.");

            await _matchService.PlayMatchAsync(model);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMatch(MatchRequestDTO model, int id)
        {
            _logger.LogInformation("Updating a match.");

            var updatedMatch = await _matchService.UpdateMatchAsync(model, id);

            return Ok(updatedMatch);
        }

        [HttpGet]
        public async Task<ActionResult<List<MatchResponseDTO>>> GetAllMatches()
        {
            _logger.LogInformation("Getting all matches.");

            var matches = await _matchService.GetAllMatchesAsync();

            return Ok(matches);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<List<MatchResponseDTO>>> GetAllMatchesByTeam(int id)
        {
            _logger.LogInformation("Getting all team matches.");

            var matches = await _matchService.GetAllMatchesByTeamAsync(id);

            return Ok(matches);
        }
       
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMatch(int id)
        {
            _logger.LogInformation("Deleting a match.");

            await _matchService.DeleteMatchAsync(id);

            return Ok();
        }
    }
}

