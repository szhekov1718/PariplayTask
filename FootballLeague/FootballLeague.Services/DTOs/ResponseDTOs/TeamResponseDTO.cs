namespace FootballLeague.Services.DTOs.ResponseDTOs
{
    public class TeamResponseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PlayedMatches { get; set; }

        public int Points { get; set; }

        public int Position { get; set; }

        public string LeagueRankingName { get; set; }
    }
}
