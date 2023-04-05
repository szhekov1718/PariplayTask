using FootballLeague.DAL.Models;
using FootballLeague.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace FootballLeague.DAL
{
    public class FootballLeagueContext : DbContext
    {
        public FootballLeagueContext(DbContextOptions<FootballLeagueContext> options) : base(options) 
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<LeagueRanking> LeagueRankings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.SeedData();
        }
    }
}
