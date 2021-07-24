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
using Infrastructure.Data;
using Xunit;

namespace API.tests
{
    public class AdvertisementControllerTest
    {
        private Mapper CreateMapper()
        {
            var mapProfile = new MappingProfiles();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(mapProfile));
            var mapper = new Mapper(config);

            return mapper;
        }

        [Fact]
        public async void Add_Click_Returns_The_Answer_From_Db()
        {
            // Arrange
            int id = 1;

            var fakeAd = A.CollectionOfDummy<Advertisement>(id).First();
            var dataStore = A.Fake<IUnitOfWork>();
            A.CallTo(() => dataStore.Repository<Advertisement>().GetByIdAsync(id))
                .Returns(Task.FromResult(fakeAd));

            var mapper = CreateMapper();

            var statService = new StatisticService(dataStore);
            var adService = new AdvertisementService(dataStore);

            var adToCreate = new AdvertisementToCreate
            {
                Category = "Toys",
                Content = "content",
                Cost = 2,
                Name = "Name",
                Type = AdType.TextAd
            };

            var controller = new AdvertisementController(dataStore, mapper, statService, adService);
            // Act

            var actionResult = await controller.CreateAd(adToCreate);
            // Assert

            var result = actionResult.Result as OkObjectResult;
            var returnInteger = result.Value;

        }
    }
}
