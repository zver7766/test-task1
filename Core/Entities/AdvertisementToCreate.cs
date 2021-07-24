using System;

namespace Core.Entities
{
    public class AdvertisementToCreate
    {
        public AdType Type { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Cost { get; set; }
        public string Content { get; set; }
    }
}
