using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class CategoryByIdSpecification : BaseSpecification<Category>
    {
        public CategoryByIdSpecification(string name)
            :base(x => x.Name == name)
        {
            
        }
    }
}
