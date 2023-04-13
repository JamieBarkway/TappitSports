using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TappitSports.Data;
using TappitSports.DataContext;
using TappitSports.Interfaces;

namespace TappitSports.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleWithFavouriteSportsController : ControllerBase
    {
        private readonly ISportDataContext context;
        private readonly ILogger<PeopleWithFavouriteSportsController> logger;

        public PeopleWithFavouriteSportsController(ISportDataContext context, ILogger<PeopleWithFavouriteSportsController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        /// <summary>
        /// Returns a list of people along with their favourite sports
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetPeopleWithFavouriteSports")]
        public async Task<ActionResult> GetPeopleWithFavouriteSports()
        {
            try
            {
                var peopleWithFavoriteSports = await (from person in context.People
                                                      join favoriteSport in context.FavoriteSport
                                                      on person.personid equals favoriteSport.personid
                                                      join sport in context.Sports
                                                      on favoriteSport.sportid equals sport.sportid
                                                      group sport.name by new
                                                      {
                                                          person.personid,
                                                          person.firstname,
                                                          person.lastname,
                                                          person.isvalid,
                                                          person.isenabled,
                                                          person.isauthorised
                                                      } into g
                                                      select new PersonWithFavouriteSports
                                                      {
                                                          FirstName = g.Key.firstname,
                                                          LastName = g.Key.lastname,
                                                          FavouriteSports = g.ToList(),
                                                          isValid = g.Key.isvalid,
                                                          isEnabled = g.Key.isenabled,
                                                          isAuthorised = g.Key.isauthorised
                                                      }).ToListAsync();

                logger.LogInformation("{count} people found along with their favourite sports", peopleWithFavoriteSports.Count);
                return Ok(peopleWithFavoriteSports);
            }
            catch (InvalidOperationException)
            {
                logger.LogError("No people found");
                return NotFound();
            }
        }
    }
}