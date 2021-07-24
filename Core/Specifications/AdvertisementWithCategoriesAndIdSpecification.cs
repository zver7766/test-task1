using Core.Entities;

namespace Core.Specifications
{
    public class AdvertisementWithCategoriesAndIdSpecification : BaseSpecification<Advertisement>
    {
        public AdvertisementWithCategoriesAndIdSpecification(int adId)
            : base(x =>
                x.IsActive && (x.Id == adId))
        {
            AddInclude(x => x.Category);
        }
    }
}
