using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using API.Commands;
using Core.Entities;
using Core.Intefraces;
using Core.Specifications;
using MediatR;

namespace API.Handlers
{
    public class AdClickHandler : IRequestHandler<AdClickCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdClickHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(AdClickCommand request, CancellationToken cancellationToken)
        {
            var spec = new AdvertisementWithCategoriesAndIdSpecification(request.Id);
            var ad = await _unitOfWork.Repository<Advertisement>().GetEntityWithSpec(spec);

            if (ad == null) return 0;

            ad.ViewsCount += 1;
            ad.Clicks += 1;

            _unitOfWork.Repository<Advertisement>().Update(ad);

            var result = await _unitOfWork.Complete();

            return result;
        }
    }
}
