using System.Collections.Generic;

namespace Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Advertisement> Advertisements { get; set; } = new();
    }
}
