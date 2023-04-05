using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Services.DTOs.RequestDTOs
{
    public class TeamRequestDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int LeagueRankingId { get; set; }
    }
}
