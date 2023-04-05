using FootballLeague.Services.Contracts;
using FootballLeague.Services.DTOs.RequestDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FootballLeague.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly ILogger _logger;

        public TeamController(ITeamService teamService, ILogger<TeamController> logger)
        {
            _teamService = teamService;
            _logger = logger;
        }
     
        [HttpPost]
        public async Task<ActionResult> CreateTeam(TeamRequestDTO model)
        {
            _logger.LogInformation("Creating a team.");

            var newTeam = await _teamService.CreateTeamAsync(model);

            return Ok(newTeam);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTeam(TeamRequestDTO model, int id)
        {
            _logger.LogInformation("Updating a team.");

            var updatedTeam = await _teamService.UpdateTeamAsync(model, id);

            return Ok(updatedTeam);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam(int id)
        {
            _logger.LogInformation("Deleting a team.");

            await _teamService.DeleteTeamAsync(id);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTeamById(int id)
        {
            _logger.LogInformation("Getting a team by Id.");

            var team = await _teamService.GetTeamByIdAsync(id);

            return Ok(team);
        }
    }
}
