using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AdvertisementToReturnDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Cost { get; set; }
        public string Content { get; set; }
        public int ViewsCount { get; set; }
        public bool IsActive { get; set; }
        public int Clicks { get; set; }
    }
}
