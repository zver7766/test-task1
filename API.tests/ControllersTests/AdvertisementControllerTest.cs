using System.Threading.Tasks;
using API.Commands;
using API.Controllers;
using API.Handlers;
using API.Tests.Helpers;
using Core.Entities;
using Core.Intefraces;
using Core.Interfaces;
using MediatR;
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

             var mockAdService = new Mock<IAdvertisementService>();

            var newCreateAdParams = new AdvertisementToCreate();

            var mapper = TestHelper.CreateMapper();

            var mediator = new Mock<IMediator>();
            var command = new CreateAdCommand(newCreateAdParams);
            var handler = new CreateAdHandler(mapper,mockAdService.Object);

            var controller = new AdvertisementController(mediator.Object);

            controller.ModelState.AddModelError("Name", "Required");
            controller.ModelState.AddModelError("Content", "Required");
            controller.ModelState.AddModelError("Category", "Required");


            // Act

            var result = await controller.CreateAd(newCreateAdParams);
            // Assert
            var viewResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400,viewResult?.StatusCode);
            Assert.Equal("Add was not created", viewResult?.Value);

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

            var mediator = new Mock<IMediator>();
            var command = new CreateAdCommand(addToCreateParams);
            var handler = new CreateAdHandler(mapper, mockAdService.Object);


            var controller = new AdvertisementController(mediator.Object);

            // Act

            var result = await controller.CreateAd(addToCreateParams);
            // Result
            var viewResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, viewResult?.StatusCode);
            Assert.Equal("Add was not created", viewResult?.Value);
        }

    }
}
