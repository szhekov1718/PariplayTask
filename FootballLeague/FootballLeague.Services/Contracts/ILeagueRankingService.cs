using FootballLeague.DAL.Models;
using FootballLeague.Services.DTOs.RequestDTOs;
using FootballLeague.Services.DTOs.ResponseDTOs;
using System.Threading.Tasks;

namespace FootballLeague.Services.Contracts
{
    public interface ILeagueRankingService
    {
        Task<LeagueRanking> CreateLeagueRankingAsync(LeagueRankingRequestDTO leagueModel);

        Task<LeagueRankingResponseDTO> GetLeagueRankingScoreboardAsync(int id);

        Task LeagueRankingExistsValidator(int id);
    }
}
