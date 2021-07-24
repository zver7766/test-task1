using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Intefraces;
using Core.Specifications;

namespace Infrastructure.Data
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdvertisementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Advertisement> CreateAdAsync(AdvertisementToCreate addCreateParams)
        {
            var categories = await _unitOfWork.Repository<Category>().ListAllAsync();
            var categoriesNames = categories.Select(x => x.Name);
            if (!categoriesNames.Contains(addCreateParams.Category))
            {
                _unitOfWork.Repository<Category>().Add(new Category {Name = addCreateParams.Category});
                await _unitOfWork.Complete();
            }


            var categoriesToFilter = await _unitOfWork.Repository<Category>().ListAllAsync();
            var category = categoriesToFilter.FirstOrDefault(x => x.Name == addCreateParams.Category);

            var isValidUrl = true;

            if (addCreateParams.Type is AdType.BannerAd or AdType.VideoAd)
                isValidUrl = Uri.IsWellFormedUriString(addCreateParams.Name, UriKind.RelativeOrAbsolute);

            Advertisement ad;
            if (isValidUrl)
            { 
                ad = new Advertisement
                {
                    Content = addCreateParams.Content,
                    Cost = addCreateParams.Cost,
                    IsActive = true,
                    Name = addCreateParams.Name,
                    Type = addCreateParams.Type,
                    ViewsCount = 0,
                    CategoryId = category.Id == null ? 0 : category.Id,
                    Clicks = 0
                };
                _unitOfWork.Repository<Advertisement>().Add(ad);

                await _unitOfWork.Complete();

                return ad;
            }

            return ad = new Advertisement
            {
                Name = "406",
            };
        }
    }
}
