using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class AdvertisementOrderBySpecParams
    {
        public int Count { get; set; } = 3;
        public OrderByCriteria? OrderByCriteria { get; set; }
    }

    public enum OrderByCriteria
    {
        [EnumMember(Value = "Categories")]
        CategoriesAsc,
        [EnumMember(Value = "Posts")]
        PostsAsc,
        [EnumMember(Value = "Types")]
        TypesAsc
    }
}
