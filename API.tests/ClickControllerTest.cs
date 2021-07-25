using API.Controllers;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Intefraces;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using API.Tests.Helpers;
using Core.Specifications;
using Moq;
using Xunit;

namespace API.Tests
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

        [Fact]
        public async void Click_Ad_Increase_Clicks_And_Views_Returns_Int_From_Db()
        {
            // Arrange
            const int Id = 1;

            var entity = new Advertisement
            {
                Type= AdType.HtmlAd ,
                Name= "Bombastick",
                CategoryId= 1,
                Cost= 2,
                Content="It`s a super toy",
                ViewsCount= 30,
                IsActive= true,
                Clicks=  3
            };

            var spec = new AdvertisementWithCategoriesAndIdSpecification(Id);
            var mockDb = new Mock<IUnitOfWork>();
            mockDb.Setup(db => db.Repository<Advertisement>().GetEntityWithSpec(spec))
                .Returns(Task.FromResult(entity));

            var controller = new ClickController(mockDb.Object);

            // Act

            var result = await controller.AdClick(Id);
            // Assert

        }
    }
}
