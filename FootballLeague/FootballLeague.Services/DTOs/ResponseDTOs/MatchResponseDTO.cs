namespace FootballLeague.Services.DTOs.ResponseDTOs
{
    public class MatchResponseDTO
    {
        public int Id { get; set; }

        public string AwayTeamName { get; set; }

        public int AwayTeamScore { get; set; }

        public string HomeTeamName { get; set; }

        public int HomeTeamScore { get; set; }

        public string LeagueRankingName { get; set; }
    }
}
