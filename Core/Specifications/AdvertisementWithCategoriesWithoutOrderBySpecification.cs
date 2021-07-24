using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class AdvertisementWithCategoriesOnlyOrderBySpecification : BaseSpecification<Advertisement>
    {
        public AdvertisementWithCategoriesOnlyOrderBySpecification()
            :base(x=> x.IsActive)
        {
            AddInclude(x => x.Category);

            AddOrderByDescending(p => p.ViewsCount);
        }
        
    }
}
