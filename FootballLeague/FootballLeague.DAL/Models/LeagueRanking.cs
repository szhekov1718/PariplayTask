using FootballLeague.DAL.Models.BaseModel;
using System.Collections.Generic;

namespace FootballLeague.DAL.Models
{
    public class LeagueRanking : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Team> Teams { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
