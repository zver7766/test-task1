using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Intefraces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClickController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClickController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("AdClick")]
        public async Task<ActionResult> AdClick(int id)
        {
            var spec = new AdvertisementWithCategoriesAndIdSpecification(id);
            var ad = await _unitOfWork.Repository<Advertisement>().GetEntityWithSpec(spec);

            if (ad == null) return NotFound("Ad was not found / Ad with this id inactive");

            ad.ViewsCount += 1;
            ad.Clicks += 1;

            _unitOfWork.Repository<Advertisement>().Update(ad);

            var result = await _unitOfWork.Complete();

            return Ok(result);
        }
    }
}
