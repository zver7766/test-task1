using System.Collections.Generic;
using API.Dtos;
using Core.Specifications;
using MediatR;

namespace API.Queries
{
    public class StatsQuery : IRequest<IReadOnlyList<StatisticToReturnDto>>
    {
        public AdvertisementOrderBySpecParams AdOrderBySpec { get; }
        public StatsQuery(AdvertisementOrderBySpecParams adOrderBySpec)
        {
            AdOrderBySpec = adOrderBySpec;
        }

    }
}
