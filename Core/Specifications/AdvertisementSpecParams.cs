using Core.Entities;

namespace Core.Specifications
{
    public class AdvertisementSpecParams
    {
        public AdType? Type { get; set; }
        public int? CategoryId { get; set; }
        public string Sort { get; set; }
    }
}
