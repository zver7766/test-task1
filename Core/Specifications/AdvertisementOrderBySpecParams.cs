using System.Runtime.Serialization;

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
