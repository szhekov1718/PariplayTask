using System.Collections.Generic;

namespace FootballLeague.Services.DTOs.ResponseDTOs
{
    public class LeagueRankingResponseDTO
    {
        public string Name { get; set; }
       
        public ICollection<TeamResponseDTO> Teams { get; set; }
    }
}
