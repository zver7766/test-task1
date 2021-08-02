using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Dtos;
using API.Queries;
using AutoMapper;
using Core.Entities;
using Core.Intefraces;
using Core.Specifications;
using MediatR;

namespace API.Handlers
{
    public class GetAdByCriteriaHandler : IRequestHandler<GetAdByCriteriaQuery, IReadOnlyList<AdvertisementToReturnDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAdByCriteriaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<AdvertisementToReturnDto>> Handle(GetAdByCriteriaQuery request, CancellationToken cancellationToken)
        {
            var spec = new AdvertisementWithCategoriesSpecification(request.AdParams);

            var ad = await _unitOfWork.Repository<Advertisement>().ListAsync(spec);

            if (ad == null || ad.Count == 0) return null;

            foreach (var item in ad)
            {
                item.ViewsCount += 1;
                _unitOfWork.Repository<Advertisement>().Update(item);
            }

            await _unitOfWork.Complete();

            var mappedAd = _mapper.Map<IReadOnlyList<Advertisement>,
                IReadOnlyList<AdvertisementToReturnDto>>(ad);

            return mappedAd;
        }
    }
}
