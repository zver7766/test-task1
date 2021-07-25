using System.Threading.Tasks;
using API.Controllers;
using API.Tests.Helpers;
using Core.Entities;
using Core.Intefraces;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace API.Tests.ControllersTests
{
    public class AdvertisementControllerTest
    {

        [Fact]
        public async void Add_Ad_Returns_View_Result_With_User_Model()
        { 
            // Arrange
            var mockDb = new Mock<IUnitOfWork>();
            var mockStatService = new Mock<IStatisticService>();
            var mockAdService = new Mock<IAdvertisementService>();

            var mapper = TestHelper.CreateMapper();
            var controller = new AdvertisementController(mockDb.Object,mapper,mockStatService.Object,
                mockAdService.Object);

            controller.ModelState.AddModelError("Name", "Required");
            controller.ModelState.AddModelError("Content", "Required");
            controller.ModelState.AddModelError("Category", "Required");

            var newCreateAdParams = new AdvertisementToCreate();

            // Act

            var result = await controller.CreateAd(newCreateAdParams);
            // Assert
            var viewResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400,viewResult?.StatusCode);
            Assert.Equal("Not correct data", viewResult?.Value);

        }

        [Fact]
        public async void Add_Ad_Returns_406()
        {
            // Arrange
            var mockDb = new Mock<IUnitOfWork>();

            var mockStatService = new Mock<IStatisticService>();

            var addToCreateParams = new AdvertisementToCreate
            {
                Name = "123",
                Category = "Toys",
                Cost = 10,
                Content = "Content",
                Type = AdTypeToCreate.BannerAd
            };

            var returnsIncorrectAdd = new Advertisement
            {
                Name = "406"
            };


            var mockAdService = new Mock<IAdvertisementService>();
            mockAdService.Setup(adService => adService.CreateAdAsync(addToCreateParams))
                .Returns(Task.FromResult(returnsIncorrectAdd));

            var mapper = TestHelper.CreateMapper();

            var controller = new AdvertisementController(mockDb.Object, mapper, mockStatService.Object,
                mockAdService.Object);

            // Act

            var result = await controller.CreateAd(addToCreateParams);
            // Result
            var viewResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, viewResult?.StatusCode);
            Assert.Equal("Ad does not created or Url not correct", viewResult?.Value);
        }

    }
}
