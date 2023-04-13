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
    public class PeopleWithFavouriteSportsControllerTests
    {
        [TestMethod]
        public async Task GetPeopleWithFavouriteSports_ReturnsOkResultWithFavouriteSportList()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person { personid = 1, firstname = "Jamie", lastname = "Barkway", isauthorised = true, isvalid = true, isenabled = false},
                new Person { personid = 2, firstname = "John", lastname = "Doe", isauthorised = false, isvalid = false, isenabled = true},
                new Person { personid = 3, firstname = "Jurgen", lastname = "Klopp", isauthorised = true, isvalid = true, isenabled = true},
            };

            var sports = new List<Sport>
            {
                new Sport { sportid = 1, name = "American Football" },
                new Sport { sportid = 2, name = "Basketball" },
                new Sport { sportid = 3, name = "Baseball" },
            };
            var favoriteSports = new List<FavouriteSport>
            {
                new FavouriteSport { sportid = 1, personid = 1 },
                new FavouriteSport { sportid = 1, personid = 2 },
                new FavouriteSport { sportid = 2, personid = 1  },
                new FavouriteSport { sportid = 2, personid = 3  },
                new FavouriteSport { sportid = 2, personid = 2  },
                new FavouriteSport { sportid = 3, personid = 2  },
            };

            var mockDbContext = new Mock<ISportDataContext>();
            mockDbContext.Setup(x => x.People).ReturnsDbSet(people);
            mockDbContext.Setup(x => x.Sports).ReturnsDbSet(sports);
            mockDbContext.Setup(x => x.FavoriteSport).ReturnsDbSet(favoriteSports);

            var logger = Mock.Of<ILogger<PeopleWithFavouriteSportsController>>();
            var controller = new PeopleWithFavouriteSportsController(mockDbContext.Object, logger);

            // Act
            var result = await controller.GetPeopleWithFavouriteSports();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOfType(okResult.Value, typeof(List<PersonWithFavouriteSports>));

            var peopleWithFavouriteSportsCountList = (List<PersonWithFavouriteSports>)okResult.Value;
            Assert.AreEqual(3, peopleWithFavouriteSportsCountList.Count);

            var jamieBarkway = peopleWithFavouriteSportsCountList.FirstOrDefault(x => x.FirstName == "Jamie" && x.LastName == "Barkway");
            Assert.IsNotNull(jamieBarkway);
            Assert.AreEqual(2, jamieBarkway.FavouriteSports.Count);
            Assert.IsTrue(jamieBarkway.FavouriteSports.Contains("American Football"));
            Assert.IsTrue(jamieBarkway.FavouriteSports.Contains("Basketball"));
            Assert.IsFalse(jamieBarkway.FavouriteSports.Contains("Baseball"));
            Assert.IsTrue(jamieBarkway.isValid);
            Assert.IsTrue(jamieBarkway.isAuthorised);
            Assert.IsFalse(jamieBarkway.isEnabled);

            var johnDoe = peopleWithFavouriteSportsCountList.FirstOrDefault(x => x.FirstName == "John" && x.LastName == "Doe");
            Assert.IsNotNull(johnDoe);
            Assert.AreEqual(3, johnDoe.FavouriteSports.Count);
            Assert.IsTrue(johnDoe.FavouriteSports.Contains("American Football"));
            Assert.IsTrue(johnDoe.FavouriteSports.Contains("Basketball"));
            Assert.IsTrue(johnDoe.FavouriteSports.Contains("Baseball"));
            Assert.IsFalse(johnDoe.isValid);
            Assert.IsFalse(johnDoe.isAuthorised);
            Assert.IsTrue(johnDoe.isEnabled);

            var jurgenKlopp = peopleWithFavouriteSportsCountList.FirstOrDefault(x => x.FirstName == "Jurgen" && x.LastName == "Klopp");
            Assert.IsNotNull(jurgenKlopp);
            Assert.AreEqual(1, jurgenKlopp.FavouriteSports.Count);
            Assert.IsFalse(jurgenKlopp.FavouriteSports.Contains("American Football"));
            Assert.IsTrue(jurgenKlopp.FavouriteSports.Contains("Basketball"));
            Assert.IsFalse(jurgenKlopp.FavouriteSports.Contains("Baseball"));
            Assert.IsTrue(jurgenKlopp.isValid);
            Assert.IsTrue(jurgenKlopp.isAuthorised);
            Assert.IsTrue(jurgenKlopp.isEnabled);
        }
    }
}