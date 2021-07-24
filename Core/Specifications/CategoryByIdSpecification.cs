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
