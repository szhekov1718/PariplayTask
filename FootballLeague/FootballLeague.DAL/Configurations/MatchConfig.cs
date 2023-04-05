using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FootballLeague.DAL.Models;

namespace FootballLeague.DAL.Configurations
{
    public class MatchConfig : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> match)
        {
            match.HasKey(m => m.Id);

            match.HasOne(m => m.AwayTeam).WithMany(m => m.AwayMatchesPlayed).HasForeignKey(m => m.AwayTeamId).OnDelete(DeleteBehavior.Restrict);

            match.HasOne(m => m.HomeTeam).WithMany(m => m.HomeMatchesPlayed).HasForeignKey(m => m.HomeTeamId).OnDelete(DeleteBehavior.Restrict);

            match.Property(m => m.AwayTeamScore).IsRequired();

            match.Property(m => m.HomeTeamScore).IsRequired();
        }
    }
}
