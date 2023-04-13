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
    public class SportsWithFavouriteCountControllerTests
    {
        [TestMethod]
        public async Task GetSportsWithFavouriteCount_ReturnsOkResultWithSportFavouriteCountList()
        {
            // Arrange
            var sports = new List<Sport>
            {
                new Sport { sportid = 1, name = "American Football" },
                new Sport { sportid = 2, name = "Basketball" },
                new Sport { sportid = 3, name = "Baseball" },
            };
            var favoriteSports = new List<FavouriteSport>
            {
                new FavouriteSport { sportid = 1 },
                new FavouriteSport { sportid = 1 },
                new FavouriteSport { sportid = 2 },
                new FavouriteSport { sportid = 2 },
                new FavouriteSport { sportid = 2 },
                new FavouriteSport { sportid = 3 },
            };

            var mockDbContext = new Mock<ISportDataContext>();
            mockDbContext.Setup<DbSet<Sport>>(x => x.Sports).ReturnsDbSet(sports);
            mockDbContext.Setup(x => x.FavoriteSport).ReturnsDbSet(favoriteSports);

            var logger = Mock.Of<ILogger<SportsWithFavouriteCountController>>();
            var controller = new SportsWithFavouriteCountController(mockDbContext.Object, logger);

            // Act
            var result = await controller.GetSportsWithFavouriteCount();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOfType(okResult.Value, typeof(List<SportFavouriteCount>));

            var sportFavouriteCountList = (List<SportFavouriteCount>)okResult.Value;
            Assert.AreEqual(3, sportFavouriteCountList.Count);
            Assert.AreEqual("American Football", sportFavouriteCountList[0].Name);
            Assert.AreEqual(2, sportFavouriteCountList[0].FavoriteCount);
            Assert.AreEqual("Basketball", sportFavouriteCountList[1].Name);
            Assert.AreEqual(3, sportFavouriteCountList[1].FavoriteCount);
            Assert.AreEqual("Baseball", sportFavouriteCountList[2].Name);
            Assert.AreEqual(1, sportFavouriteCountList[2].FavoriteCount);
        }
    }
}