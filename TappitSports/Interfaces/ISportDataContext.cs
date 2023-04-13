using Microsoft.EntityFrameworkCore;
using TappitSports.Data;
using TappitSports.Models;

namespace TappitSports.Interfaces
{
    public interface ISportDataContext
    {
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<FavouriteSport> FavoriteSport { get; set; }
    }
}
