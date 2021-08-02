using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Dtos;
using Core.Specifications;
using MediatR;

namespace API.Queries
{
    public class GetAdByCriteriaQuery : IRequest<IReadOnlyList<AdvertisementToReturnDto>>
    {
        public AdvertisementSpecParams AdParams { get; }
        public GetAdByCriteriaQuery(AdvertisementSpecParams adParams)
        {
            AdParams = adParams;
        }
    }
}
