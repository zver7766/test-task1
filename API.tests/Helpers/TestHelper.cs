using System.Collections.Generic;
using API.Helpers;
using AutoMapper;
using Core.Entities;

namespace API.Tests.Helpers
{
    public static class TestHelper
    {
        public static Mapper CreateMapper()
        {
            var mapProfile = new MappingProfiles();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(mapProfile));
            var mapper = new Mapper(config);

            return mapper;
        }

        public static IEnumerable<Advertisement> GetListOfAds()
        {
            var category = new Category
            {
                Id = 1,
                Name = "Toys"
            };
            Advertisement[] entities =
            {
                new()
                {
                    Id = 1,
                    Type = AdType.TextAd,
                    Name = "Bombastick",
                    CategoryId = 1,
                    Cost = 2,
                    Content = "It`s a super toy",
                    ViewsCount = 30,
                    IsActive = true,
                    Clicks = 3,
                    Category = category

                },
                new()
                {
                    Id = 2,
                    Type = AdType.HtmlAd,
                    Name = "<p>Paragraph</p>",
                    CategoryId = 1,
                    Cost = 322,
                    Content = "<a>It`s a super toy</a>",
                    ViewsCount = 3330,
                    IsActive = true,
                    Clicks = 333,
                    Category = category
                }

            };

            List<Advertisement> listOfEntities = new List<Advertisement>();
            listOfEntities.AddRange(entities);

            return listOfEntities;
        }
    }
}
