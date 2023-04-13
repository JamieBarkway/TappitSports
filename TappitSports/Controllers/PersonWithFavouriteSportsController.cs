using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TappitSports.Data;
using TappitSports.DataContext;
using TappitSports.Interfaces;
using TappitSports.Models;

namespace TappitSports.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonWithFavouriteSportsController : ControllerBase
    {
        private readonly ISportDataContext context;
        private readonly ILogger<PersonWithFavouriteSportsController> logger;
        public PersonWithFavouriteSportsController(ISportDataContext context, ILogger<PersonWithFavouriteSportsController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        /// <summary>
        /// Returns a person and their favourite sports based on the provided person Id OR last name
        /// </summary>
        /// <param name="personId">
        /// <description>
        /// The ID of the person you wish to retrieve. Will override last name if ID and last name do not match.
        /// </description>
        /// </param>
        /// <param name="personLastName">
        /// <description>
        /// The last name of the person you wish to retrieve
        /// </description>
        /// </param>
        /// <returns></returns>
        [HttpGet(Name = "GetPersonWithFavouriteSport")]
        public async Task<ActionResult> GetPersonWithFavouriteSport([FromQuery] int? personId = -1, [FromQuery] string? personLastName = "")
        {
            try
            {
                var personWithFavouriteSports = await (from person in context.People
                                                       join favouriteSport in context.FavoriteSport
                                                       on person.personid equals favouriteSport.personid
                                                       join sport in context.Sports
                                                       on favouriteSport.sportid equals sport.sportid
                                                       where personId > 0 ? person.personid == personId 
                                                        : person.lastname.ToLower() == personLastName.ToLower()
                                                       group sport.name by new
                                                       {
                                                           person.personid,
                                                           person.firstname,
                                                           person.lastname,
                                                       } into g
                                                       select new PersonWithFavouriteSports
                                                       {
                                                           FirstName = g.Key.firstname,
                                                           LastName = g.Key.lastname,
                                                           FavouriteSports = g.ToList()
                                                       }).FirstOrDefaultAsync();

                if (personWithFavouriteSports == null) throw new InvalidOperationException();
                logger.LogInformation("Person {firstname} {lastname} found with favourite sports {favouritesports}",
                    personWithFavouriteSports.FirstName, personWithFavouriteSports.LastName, personWithFavouriteSports.FavouriteSports);
                return Ok(personWithFavouriteSports);
            }
            catch (InvalidOperationException)
            {
                logger.LogError("Person not found");
                return NotFound();
            }
        }
    }
}