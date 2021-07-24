namespace Core.Entities
{
    public class Advertisement : BaseEntity
    {
        public AdType Type { get; set; } = AdType.TextAd;
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public decimal Cost { get; set; }
        public string Content { get; set; }
        public int ViewsCount { get; set; }
        public bool IsActive { get; set; } = true;
        public int Clicks { get; set; }
    }
}
