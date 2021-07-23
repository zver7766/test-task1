using Core.Entities;

namespace Core.Specifications
{
    public class AdvertisementWithCategoriesSpecification : BaseSpecification<Advertisement>
    {
        public AdvertisementWithCategoriesSpecification(AdvertisementSpecParams adParams)
         :base(x =>
            (!adParams.CategoryId.HasValue || x.CategoryId == adParams.CategoryId))
        {
            AddInclude(x => x.Category);

            if (!string.IsNullOrEmpty(adParams.Sort))
            {
                switch (adParams.Sort)
                {
                    case "viewsAsc":
                        AddOrderBy(p => p.ViewsCount);
                        break;
                    case "viewsDesc":
                        AddOrderByDescending(p => p.ViewsCount);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }
    }
}
