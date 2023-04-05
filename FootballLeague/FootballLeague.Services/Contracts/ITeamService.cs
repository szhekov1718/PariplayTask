using FootballLeague.DAL.Models;
using FootballLeague.Services.DTOs.RequestDTOs;
using System.Threading.Tasks;

namespace FootballLeague.Services.Contracts
{
    public interface ITeamService
    {
        Task<Team> CreateTeamAsync(TeamRequestDTO teamModel);
        Task<Team> UpdateTeamAsync(TeamRequestDTO teamModel, int id);
        Task<bool> DeleteTeamAsync(int id);
        Task TeamExistsValidator(int teamId, int leagueRankingId);
        Task<Team> GetTeamByIdAsync(int id);
    }
}
