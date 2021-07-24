using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class AdvertisementWithCategoriesAndIdSpecification : BaseSpecification<Advertisement>
    {
        public AdvertisementWithCategoriesAndIdSpecification(int adId)
            : base(x =>
                x.IsActive && (x.Id == adId))
        {

        }
    }
}
