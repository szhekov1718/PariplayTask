using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Services.DTOs.RequestDTOs
{
    public class LeagueRankingRequestDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
