using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class AdvertisementToCreate
    {
        [Required]
        public AdTypeToCreate Type { get; set; } = AdTypeToCreate.Auto;
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required]
        [MaxLength(60)]
        public string Category { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        [MaxLength(150)]
        public string Content { get; set; }
    }
}
