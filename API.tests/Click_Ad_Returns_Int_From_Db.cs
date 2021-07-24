using API.Controllers;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Intefraces;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class ClickControllerTest
    {
        [Fact]
        public async void Click_Ad_Returns_Int_From_Db()
        {
            // Arrange
            int id = 1;

            var mapProfile = new MappingProfiles();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(mapProfile));
            var mapper = new Mapper(config);

            var fakeAd = A.CollectionOfDummy<Advertisement>(id).First();
            var dataStore = A.Fake<IUnitOfWork>();
            A.CallTo(() => dataStore.Repository<Advertisement>().GetByIdAsync(id))
                .Returns(Task.FromResult(fakeAd));
            var controller = new ClickController(dataStore, mapper);
            // Act

            var actionResult = await controller.AdClick(id);
            // Assert

            var result = actionResult.Result as OkObjectResult;
            var returnInteger = result.Value;

            Assert.Equal(0, returnInteger);
        }
    }
}
