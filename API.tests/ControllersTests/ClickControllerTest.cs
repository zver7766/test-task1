using System.Threading.Tasks;
using API.Commands;
using API.Controllers;
using API.Handlers;
using Core.Entities;
using Core.Intefraces;
using Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace API.Tests.ControllersTests
{
    public class ClickControllerTest
    {
        [Fact]
        public async void Click_Ad_Returns_404()
        {
            // Arrange
            const int Id = 1;

            var spec = new AdvertisementWithCategoriesAndIdSpecification(Id);
            var mockDb = new Mock<IUnitOfWork>();
            mockDb.Setup(db => db.Repository<Advertisement>().GetEntityWithSpec(spec))
                .Returns(Task.FromResult(null as Advertisement));

            var mediator = new Mock<IMediator>();
            var command = new AdClickCommand(Id);
            var handler = new AdClickHandler(mockDb.Object);

            var controller = new ClickController(mediator.Object);
            // Act

            var result = await controller.AdClick(Id);
            // Assert

            var viewResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(0, result.Value);
            Assert.Equal(404, viewResult?.StatusCode);
            Assert.Equal("Ad was not found / Ad with this id inactive", viewResult?.Value);
        }

    }
}
