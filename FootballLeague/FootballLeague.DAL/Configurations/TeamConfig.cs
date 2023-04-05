using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FootballLeague.DAL.Models;

namespace FootballLeague.DAL.Configurations
{
    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> team)
        {
            team.HasKey(t => t.Id);

            team.HasIndex(t => t.Name).IsUnique();

            team.Property(t => t.Name).IsRequired().HasMaxLength(50);
        }
    }
}
