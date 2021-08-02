using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Dtos;
using MediatR;

namespace API.Queries
{
    public class GetAdQuery : IRequest<AdvertisementToReturnDto>
    {
        
    }
}
