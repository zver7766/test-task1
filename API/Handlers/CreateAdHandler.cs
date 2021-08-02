using API.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using API.Commands;
using AutoMapper;
using Core.Entities;
using Core.Intefraces;

namespace API.Handlers
{
    public class CreateAdHandler : IRequestHandler<CreateAdCommand, AdvertisementToReturnDto>
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IMapper _mapper;

        public CreateAdHandler(IMapper mapper, IAdvertisementService advertisementService)
        {
            _mapper = mapper;
            _advertisementService = advertisementService;
        }

        public async Task<AdvertisementToReturnDto> Handle(CreateAdCommand request,CancellationToken cancellationToken)
        {
            if (request.AdToCreate == null)
                return null;
            

            var createdAd = await _advertisementService.CreateAdAsync(request.AdToCreate);

            if (createdAd == null || createdAd.Name == "406")
                return null;

            var mapperAd = _mapper.Map<Advertisement, AdvertisementToReturnDto>(createdAd);

            return mapperAd;
        }
    }
}
