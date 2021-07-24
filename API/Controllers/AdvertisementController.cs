using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Intefraces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace API.Controllers
{
    public class AdvertisementController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStatisticService _statisticService;
        private readonly IAdvertisementService _advertisementService;

        public AdvertisementController(IUnitOfWork unitOfWork, IMapper mapper, IStatisticService statisticService,
            IAdvertisementService advertisementService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _statisticService = statisticService;
            _advertisementService = advertisementService;
        }

        [HttpPost("CreateAd")]
        public async Task<ActionResult<AdvertisementToReturnDto>> CreateAd([FromQuery] AdvertisementToCreate adCreateParams)
        {
            if (adCreateParams == null) return BadRequest("Not correct data");

           var createdAd = await _advertisementService.CreateAdAsync(adCreateParams);

           if (createdAd == null) return BadRequest("Ad does not created");

           var mapperAd = _mapper.Map<Advertisement,AdvertisementToReturnDto>(createdAd);

           return Ok(mapperAd);
        }

        [HttpGet("GetAd")]
        public async Task<ActionResult<AdvertisementToReturnDto>> GetAd()
        {
            var ads = await _unitOfWork.Repository<Advertisement>().ListAllAsync();
            var firstAd = ads.Select(x => x.Id).FirstOrDefault();

            var spec = new AdvertisementWithCategoriesAndIdSpecification(firstAd);
            var ad = await _unitOfWork.Repository<Advertisement>().GetEntityWithSpec(spec);

            if (Request.Cookies["ad_id"] == null)
            {
                Response.Cookies.Append("ad_id",(ad.Id+1).ToString());
            }

            if (Request.Cookies["ad_id"] != null)
            {
                var cookie = Request.Cookies["ad_id"];
                Int32.TryParse(cookie, out var adId);

                spec = new AdvertisementWithCategoriesAndIdSpecification(adId);

                ad = await _unitOfWork.Repository<Advertisement>().GetEntityWithSpec(spec);

                if (ad == null)
                {
                    Response.Cookies.Delete("ad_id");
                    return Ok("You reached all the ads, making a circle. Please make request again");
                }
                
                Response.Cookies.Append("ad_id",(adId+1).ToString());
            }

            ad.ViewsCount += 1;
            await _unitOfWork.Complete();

            var mappedAd = _mapper.Map<Advertisement, AdvertisementToReturnDto>(ad);

            return Ok(mappedAd);

        }

        [HttpGet("GetAdByCriteria")]
        public async Task<ActionResult<AdvertisementToReturnDto>> GetAdByCriteria([FromQuery] AdvertisementSpecParams adParams)
        {
            var spec = new AdvertisementWithCategoriesSpecification(adParams);

            var ad = await _unitOfWork.Repository<Advertisement>().ListAsync(spec);

            if (ad == null) return NotFound("Ad was not found");

            foreach (var item in ad)
            {
                item.ViewsCount += 1;
                _unitOfWork.Repository<Advertisement>().Update(item);
            }

            await _unitOfWork.Complete();

           var mappedAd = _mapper.Map<IReadOnlyList<Advertisement>,
               IReadOnlyList<AdvertisementToReturnDto>>(ad);

            return Ok(mappedAd);
        }

        [HttpGet("DeleteAd")]
        public async Task<ActionResult> DeleteAd(int id, bool deactivate = true)
        {
            var ad = await _unitOfWork.Repository<Advertisement>().GetByIdAsync(id);

            if (ad == null) return NotFound("Ad was not found");

            if (!deactivate)
                _unitOfWork.Repository<Advertisement>().Delete(ad);

            if (deactivate)
                ad.IsActive = !ad.IsActive;
            
            var result = await _unitOfWork.Complete();

            return Ok(result);
        }

        [HttpGet("Stats")]
        public async Task<ActionResult<IReadOnlyList<StatisticToReturnDto>>> Statistics([FromQuery] AdvertisementOrderBySpecParams adOrderByParams)
        { 
            var stats = await _statisticService.GetStatisticAsync(adOrderByParams);

            var statsToReturn = 
               _mapper.Map<IEnumerable<Statistic>, IEnumerable<StatisticToReturnDto>>(stats);

            return Ok(statsToReturn);
        }


    }
}
