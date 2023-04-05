using FootballLeague.DAL.Models.BaseModel;

namespace FootballLeague.DAL.Models
{
    public class Match : BaseEntity
    {
        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; }

        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; }

        public int LeagueRankingId { get; set; }

        public LeagueRanking LeagueRanking { get; set; }

        public int AwayTeamScore { get; set; }

        public int HomeTeamScore { get; set; }

        public bool IsPlayed { get; set; }
    }
}
