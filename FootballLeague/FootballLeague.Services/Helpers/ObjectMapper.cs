using FootballLeague.DAL.Models;
using FootballLeague.Services.DTOs.RequestDTOs;
using FootballLeague.Services.DTOs.ResponseDTOs;
using System.Linq;

namespace FootballLeague.Services.Helpers
{
    public class ObjectMapper
    {
        public static Team MapToTeam(TeamRequestDTO teamDTO)
        {
            return new Team()
            {
                Name = teamDTO.Name,
                LeagueRankingId = teamDTO.LeagueRankingId,
            };
        }

        public static Match MapToMatch(MatchRequestDTO matchDTO)
        {
            return new Match()
            {
                HomeTeamId = matchDTO.HomeTeamId,
                AwayTeamId = matchDTO.AwayTeamId,
                HomeTeamScore = matchDTO.HomeTeamScore,
                AwayTeamScore = matchDTO.AwayTeamScore,
                IsPlayed = false,
                LeagueRankingId = matchDTO.LeagueRankingId
            };
        }

        public static MatchResponseDTO MapToMatchResponseDTO(Match match)
        {
            return new MatchResponseDTO()
            {
                Id = match.Id,
                LeagueRankingName = match.LeagueRanking.Name,
                HomeTeamName = match.HomeTeam.Name,
                AwayTeamName = match.AwayTeam.Name,
                AwayTeamScore = match.AwayTeamScore,
                HomeTeamScore = match.HomeTeamScore
            };
        }

        public static LeagueRankingResponseDTO MapToLeagueRankingResponseDTO(LeagueRanking leagueRanking)
        {
            return new LeagueRankingResponseDTO()
            {
                Name = leagueRanking.Name,
                Teams = leagueRanking.Teams
                .Select(t => new TeamResponseDTO()
                {
                    Id = t.Id,
                    Name = t.Name,
                    PlayedMatches = t.HomeMatchesPlayed.Count + t.AwayMatchesPlayed.Count,
                    Points = t.Points,
                    LeagueRankingName = leagueRanking.Name
                })
                .OrderByDescending(t => t.Points)
                .ToList()
            };
        }

        public static MatchRequestDTO MapToMatchRequestDTO(Match match)
        {
            return new MatchRequestDTO()
            {
                AwayTeamScore = match.AwayTeamScore,
                HomeTeamScore = match.HomeTeamScore,
            };
        }
    }
}
