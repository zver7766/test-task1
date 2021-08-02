using API.Dtos;
using Core.Entities;
using MediatR;

namespace API.Commands
{
    public class CreateAdCommand : IRequest<AdvertisementToReturnDto>
    {
        public CreateAdCommand(AdvertisementToCreate adToCreate)
        {
            AdToCreate = adToCreate;
        }

        public AdvertisementToCreate AdToCreate { get; set; }
    }
}
