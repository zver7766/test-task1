using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
