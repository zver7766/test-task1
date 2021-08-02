using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using API.Dtos;
using API.Queries;
using AutoMapper;
using Core.Entities;
using Core.Intefraces;
using Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace API.Handlers
{
    public class GetAdHandler : IRequestHandler<GetAdQuery, AdvertisementToReturnDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _context;
        private readonly IMapper _mapper;

        public GetAdHandler(IUnitOfWork unitOfWork, IHttpContextAccessor context,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
        }

        public async Task<AdvertisementToReturnDto> Handle(GetAdQuery request, CancellationToken cancellationToken)
        {
            const string CookieName = "ad_id";
            AdvertisementWithCategoriesAndIdSpecification spec;
            var ad = new Advertisement();
            
            

            if (_context.HttpContext.Request.Cookies[CookieName] == null)
            {
                var ads = await _unitOfWork.Repository<Advertisement>().ListAllAsync();
                var firstAdId = ads.Select(x => x.Id).FirstOrDefault();

                spec = new AdvertisementWithCategoriesAndIdSpecification(firstAdId);
                ad = await _unitOfWork.Repository<Advertisement>().GetEntityWithSpec(spec);

                _context.HttpContext.Response.Cookies.Append(CookieName, (ad.Id + 1).ToString());
            }

            if (_context.HttpContext.Request.Cookies[CookieName] != null)
            {
                var cookie = _context.HttpContext.Request.Cookies[CookieName];
                Int32.TryParse(cookie, out var adId);

                spec = new AdvertisementWithCategoriesAndIdSpecification(adId);

                ad = await _unitOfWork.Repository<Advertisement>().GetEntityWithSpec(spec);

                _context.HttpContext.Response.Cookies.Append(CookieName, (adId + 1).ToString());

                if (ad == null)
                {
                    _context.HttpContext.Response.Cookies.Delete(CookieName);

                    var ads = await _unitOfWork.Repository<Advertisement>().ListAllAsync();
                    ad = ads.FirstOrDefault();

                    _context.HttpContext.Response.Cookies.Append(CookieName, (ad?.Id + 1).ToString());
                }

            }

            ad.ViewsCount += 1;
            await _unitOfWork.Complete();

            var mappedAd = _mapper.Map<Advertisement, AdvertisementToReturnDto>(ad);

            return mappedAd;
        }
    }
}
