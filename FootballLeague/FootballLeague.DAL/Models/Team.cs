using FootballLeague.DAL.Models.BaseModel;
using System.Collections.Generic;

namespace FootballLeague.DAL.Models
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }

        public int Points { get; set; }

        public int LeagueRankingId { get; set; }

        public LeagueRanking LeagueRanking { get; set; }

        public ICollection<Match> HomeMatchesPlayed { get; set; }

        public ICollection<Match> AwayMatchesPlayed { get; set; }
    }
}
