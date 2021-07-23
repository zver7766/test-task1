using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Advertisement> Advertisements { get; set; } = new();
    }
}
