using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Intefraces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TestController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Route("get-this")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AdvertisementToReturnDto>>> GetThis([FromQuery] AdvertisementSpecParams adParams)
        {
            var spec = new AdvertisementWithCategoriesSpecification(adParams);

            var ads = await _unitOfWork.Repository<Advertisement>().ListAsync(spec);

           var mappedAds = _mapper.Map<IReadOnlyList<Advertisement>, IReadOnlyList<AdvertisementToReturnDto>>(ads);

            return Ok(mappedAds);
        }
    }
}
