using FootballLeague.DAL.Models;
using FootballLeague.Services.DTOs.RequestDTOs;
using FootballLeague.Services.DTOs.ResponseDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeague.Services.Contracts
{
    public interface IMatchService
    {
        Task<Match> CreateMatchAsync(MatchRequestDTO model);

        Task<Match> UpdateMatchAsync(MatchRequestDTO model, int id);

        Task<bool> DeleteMatchAsync(int id);

        Task PlayMatchAsync(MatchRequestDTO model);

        Task<List<MatchResponseDTO>> GetAllMatchesAsync();

        Task<List<MatchResponseDTO>> GetAllMatchesByTeamAsync(int teamId);
    }
}
