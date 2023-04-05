namespace FootballLeague.Services.DTOs.RequestDTOs
{
    public class MatchRequestDTO
    {
        public int Id { get; set; }

        public int AwayTeamId { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamScore { get; set; }

        public int HomeTeamScore { get; set; }

        public int LeagueRankingId { get; set; }
    }
}
