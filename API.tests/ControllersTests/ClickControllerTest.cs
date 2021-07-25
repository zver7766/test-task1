using System.Threading.Tasks;
using API.Controllers;
using Core.Entities;
using Core.Intefraces;
using Core.Specifications;
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

            var controller = new ClickController(mockDb.Object);
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
