using FootballLeague.DAL;
using FootballLeague.DAL.Models;
using FootballLeague.Services.Constants;
using FootballLeague.Services.Contracts;
using FootballLeague.Services.DTOs.RequestDTOs;
using FootballLeague.Services.DTOs.ResponseDTOs;
using FootballLeague.Services.Exceptions;
using FootballLeague.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Services.Services
{
    public class LeagueRankingService : ILeagueRankingService
    {
        private readonly FootballLeagueContext _context;
        public LeagueRankingService(FootballLeagueContext context)
        {
            _context = context;
        }

        public async Task<LeagueRanking> CreateLeagueRankingAsync(LeagueRankingRequestDTO leagueModel)
        {
            if (_context.LeagueRankings.Any(l => l.Name == leagueModel.Name && !l.IsDeleted))
            {
                throw new EntityAlreadyExistsException(ExceptionMessages.LeagueRankingAlreadyExists);
            }

            var leagueRanking = new LeagueRanking
            {
                Name = leagueModel.Name
            };

            await _context.LeagueRankings.AddAsync(leagueRanking);
            await _context.SaveChangesAsync();

            return leagueRanking;
        }

        public async Task<LeagueRankingResponseDTO> GetLeagueRankingScoreboardAsync(int id)
        {
            var leagueRankingModel = await _context.LeagueRankings
                                      .Include(lr => lr.Teams)
                                        .ThenInclude(m => m.HomeMatchesPlayed)
                                      .Include(lr => lr.Teams)
                                        .ThenInclude(m => m.AwayMatchesPlayed)
                                      .Where(lr => lr.Id == id && !lr.IsDeleted)
                                      .Select(lr => ObjectMapper.MapToLeagueRankingResponseDTO(lr))
                                      .FirstOrDefaultAsync();

            if (leagueRankingModel is not null)
            {
                var i = 0;
                foreach (var team in leagueRankingModel.Teams)
                {
                    i += 1;
                    team.Position = i;
                }

                return leagueRankingModel;
            }

            throw new EntityNotFoundException(ExceptionMessages.LeagueRankingNotFound);
        }

        public async Task LeagueRankingExistsValidator(int id)
        {
            if (!await _context.LeagueRankings.AnyAsync(lr => lr.Id == id && !lr.IsDeleted))
            {
                throw new EntityNotFoundException(ExceptionMessages.LeagueRankingNotFound);
            }
        }
    }
}
