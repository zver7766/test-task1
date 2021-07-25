using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Advertisement, AdvertisementToReturnDto>()
                .ForMember(d => d.Category,
                    o => o.MapFrom(s => s.Category.Name));
          
            CreateMap<Statistic, StatisticToReturnDto>().ReverseMap();

        }
    }
}
