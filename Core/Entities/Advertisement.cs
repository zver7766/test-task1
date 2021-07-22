using System.Collections.Generic;

namespace Core.Entities
{
    public class Advertisement
    {
        public AdType Type { get; set; } = AdType.TextAd;
        public string Name { get; set; }
        public List<Category> Categories { get; set; }
        public decimal Cost { get; set; }
        public string Content { get; set; }
        public int ViewsCount { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
