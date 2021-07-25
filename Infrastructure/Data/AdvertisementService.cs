using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Intefraces;

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

            Advertisement ad;
            var isValidUrl = true;
            var type = (AdType)addCreateParams.Type-1;

            if (addCreateParams.Type is AdTypeToCreate.Auto)
            {
                type = DefineType(addCreateParams.Name);
            }

            if (type is AdType.BannerAd)
                isValidUrl = UrlValidator.ValidateUrl(addCreateParams.Name);

            if (!isValidUrl)
                return ad = new Advertisement
                {
                    Name = "406",
                };

            ad = new Advertisement
            {
                Content = addCreateParams.Content,
                Cost = addCreateParams.Cost,
                IsActive = true,
                Name = addCreateParams.Name,
                Type = type,
                ViewsCount = 0,
                CategoryId = category.Id == null ? 0 : category.Id,
                Clicks = 0
            };
            _unitOfWork.Repository<Advertisement>().Add(ad);

            await _unitOfWork.Complete();

            return ad;

        }

        private AdType DefineType(string name)
        {
            var pattern = "<(“[^”]*”|'[^’]*’|[^'”>])*>";
            if (Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase))
                return AdType.HtmlAd;
            
            if (name.EndsWith(".mp4") || name.EndsWith(".avi"))
                return AdType.VideoAd;

            if (Uri.TryCreate(name,UriKind.Absolute,out Uri result))
                return AdType.BannerAd;

            return AdType.TextAd;
        }
    }
}
