using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TappitSports.Data;
using TappitSports.DataContext;
using TappitSports.Interfaces;

namespace TappitSports.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SportsWithFavouriteCountController : ControllerBase
    {
        private readonly ISportDataContext context;
        private readonly ILogger<SportsWithFavouriteCountController> logger;

        public SportsWithFavouriteCountController(ISportDataContext context, ILogger<SportsWithFavouriteCountController> logger) 
        {
            this.context = context;
            this.logger = logger;
        }

        /// <summary>
        /// Returns a list of sports and how many times they have been favourited
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetSportsWithFavouriteCount")]
        public async Task<ActionResult> GetSportsWithFavouriteCount()
        {
            try
            {
                var sportFavouriteCount = await (from sport in context.Sports
                                                 join favoriteSport in context.FavoriteSport
                                                 on sport.sportid equals favoriteSport.sportid
                                                 group favoriteSport by sport.name into favoriteCounts
                                                 select new SportFavouriteCount
                                                 {
                                                     Name = favoriteCounts.Key,
                                                     FavoriteCount = favoriteCounts.Count()
                                                 }).ToListAsync();

                logger.LogInformation("Retrieved {Count} sports along with the favourite count for each.", sportFavouriteCount.Count);
                return Ok(sportFavouriteCount);
            }
            catch (InvalidOperationException)
            {
                logger.LogError("No sports were found.");
                return NotFound();
            }
        }
    }
}
