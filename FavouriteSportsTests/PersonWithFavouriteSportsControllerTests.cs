using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using TappitSports.Controllers;
using TappitSports.Data;
using TappitSports.DataContext;
using TappitSports.Interfaces;
using TappitSports.Models;
//using Xunit;

namespace FavouriteSportsTests
{
    [TestClass]
    public class PersonWithFavouriteSportsControllerTests
    {
        [TestMethod]
        public async Task GetPersonWithFavouriteSportsController_ReturnsOkResultWithFavouriteSports()
        {
            // Arrange
            var people = new List<Person>
            {
                 new Person { personid = 1, firstname = "Jamie", lastname = "Barkway" },
                 new Person { personid = 2, firstname = "Jurgen", lastname = "Klopp" }
            };

            var sports = new List<Sport>
            {
                new Sport { sportid = 1, name = "American Football" },
                new Sport { sportid = 2, name = "Baseball" }
            };

            var favouriteSports = new List<FavouriteSport>
            {
                new FavouriteSport { personid = 1, sportid = 1 },
                new FavouriteSport { personid = 1, sportid = 2 },
                new FavouriteSport { personid = 2, sportid = 1 }
            };

            var mockDbContext = new Mock<ISportDataContext>();
            mockDbContext.Setup(x => x.People).ReturnsDbSet(people);
            mockDbContext.Setup(x => x.Sports).ReturnsDbSet(sports);
            mockDbContext.Setup(x => x.FavoriteSport).ReturnsDbSet(favouriteSports);

            var logger = Mock.Of<ILogger<PersonWithFavouriteSportsController>>();
            var controller = new TappitSports.Controllers.PersonWithFavouriteSportsController(mockDbContext.Object, logger);

            // Act
            var result = await controller.GetPersonWithFavouriteSport(people[0].personid);
            var result2 = await controller.GetPersonWithFavouriteSport(people[1].personid, people[1].lastname);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOfType(okResult.Value, typeof(PersonWithFavouriteSports));

            var personWithFavouriteSports = (PersonWithFavouriteSports)okResult.Value;
            Assert.IsNotNull(personWithFavouriteSports);
            Assert.AreEqual(people[0].firstname, personWithFavouriteSports.FirstName);
            Assert.AreEqual(people[0].lastname, personWithFavouriteSports.LastName);
            CollectionAssert.AreEqual(new List<string> { sports[0].name, sports[1].name }, personWithFavouriteSports.FavouriteSports);

            Assert.IsInstanceOfType(result2, typeof(OkObjectResult));
            var okResult2 = (OkObjectResult)result2;
            Assert.IsInstanceOfType(okResult.Value, typeof(PersonWithFavouriteSports));

            var personWithFavouriteSports2 = (PersonWithFavouriteSports)okResult2.Value;
            Assert.IsNotNull(personWithFavouriteSports);
            Assert.AreEqual(people[1].firstname, personWithFavouriteSports2.FirstName);
            Assert.AreEqual(people[1].lastname, personWithFavouriteSports2.LastName);
            Assert.AreEqual(sports[0].name, personWithFavouriteSports2.FavouriteSports[0]);
        }
    }
}