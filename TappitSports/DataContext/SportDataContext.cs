using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TappitSports.Data;
using TappitSports.Interfaces;
using TappitSports.Models;

namespace TappitSports.DataContext
{
    public class SportDataContext : DbContext, ISportDataContext
    {
        protected readonly IConfiguration Configuration;
        public SportDataContext(DbContextOptions<SportDataContext> options, IConfiguration configuration) :
            base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("FavouriteSportsDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavouriteSport>()
                  .HasKey(m => new { m.personid, m.sportid });
        }

        public DbSet<Sport> Sports { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<FavouriteSport> FavoriteSport { get; set; }
    }
}
