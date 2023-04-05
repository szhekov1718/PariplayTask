using FootballLeague.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.DAL.Configurations
{
    public class LeagueRankingConfig : IEntityTypeConfiguration<LeagueRanking>
    {
        public void Configure(EntityTypeBuilder<LeagueRanking> leagueRanking)
        {
            leagueRanking.HasKey(r => r.Id);

            leagueRanking.HasMany(lr => lr.Teams);

            leagueRanking.HasMany(lr => lr.Matches);

            leagueRanking.HasIndex(lr => lr.Name).IsUnique();

            leagueRanking.Property(r => r.Name).IsRequired().HasMaxLength(50);
        }
    }
}
