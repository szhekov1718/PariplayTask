using FootballLeague.DAL;
using FootballLeague.DAL.Models;
using FootballLeague.Services.Constants;
using FootballLeague.Services.Contracts;
using FootballLeague.Services.DTOs.RequestDTOs;
using FootballLeague.Services.Exceptions;
using FootballLeague.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Services.Services
{
    public class TeamService : ITeamService
    {
        private readonly FootballLeagueContext _context;
        private readonly ILeagueRankingService _leagueRankingService;

        public TeamService(FootballLeagueContext footballLeagueContext, ILeagueRankingService leagueRankingService)
        {
            _context = footballLeagueContext;
            _leagueRankingService = leagueRankingService;
        }

        public async Task<Team> CreateTeamAsync(TeamRequestDTO model)
        {
            TeamRequestDTOValidator(model);

            if (_context.Teams.Any(x => x.Name == model.Name && !x.IsDeleted))
            {
                throw new EntityAlreadyExistsException(ExceptionMessages.TeamAlreadyExists);
            }

            await _leagueRankingService.LeagueRankingExistsValidator(model.LeagueRankingId);

            var team = ObjectMapper.MapToTeam(model);
            team.DateCreated = DateTime.UtcNow;

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();

            return team;
        }

        public async Task<Team> UpdateTeamAsync(TeamRequestDTO model, int id)
        {
            TeamRequestDTOValidator(model);

            var team = await GetTeamByIdAsync(id);

            if (model.Name != team.Name && _context.Teams.Any(x => x.Name == model.Name && !x.IsDeleted))
            {
                throw new EntityAlreadyExistsException(ExceptionMessages.TeamAlreadyExists);
            }

            ObjectMapper.MapToTeam(model);
            team.DateUpdated = DateTime.UtcNow;

            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<bool> DeleteTeamAsync(int id)
        {
            var team = await GetTeamByIdAsync(id);

            team.IsDeleted = true;
            team.DateDeleted = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return team.IsDeleted;
        }

        public async Task TeamExistsValidator(int teamId, int leagueRankingId)
        {
            var team = await _context.Teams
                .FirstOrDefaultAsync(t => t.Id == teamId && t.LeagueRankingId == leagueRankingId && !t.IsDeleted)
                ?? throw new EntityNotFoundException(ExceptionMessages.TeamNotFound);
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            var team = await _context.Teams
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted)
                ?? throw new EntityNotFoundException(ExceptionMessages.TeamNotFound);

            return team;
        }

        private void TeamRequestDTOValidator(TeamRequestDTO model)
        {
            if (model is null)
            {
                throw new EntityNotFoundException(ExceptionMessages.RequestModelEmpty);
            }
        }
    }
}
