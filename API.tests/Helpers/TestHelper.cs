using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Helpers;
using AutoMapper;

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
    }
}
