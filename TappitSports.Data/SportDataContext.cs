using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TappitSports.Data
{
    public class SportDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public SportDataContext(DbContextOptions<SportDataContext> options, IConfiguration configuration): 
            base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("FavouriteSportsDb"));
        }

        public DbSet<Sport> Sports { get; set; }
        public DbSet<Person> People { get; set; }
    }
}
