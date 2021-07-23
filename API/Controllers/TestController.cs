using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TestController : BaseApiController
    {
        private readonly AdContext _context;
        
        public TestController(AdContext context)
        {
            _context = context;
        }
        [Route("get-this")]
        [HttpGet]
        public ActionResult GetThis()
        {
             _context.Categories.Add(new Category
             {
                 Name = "Toys"
             });
            _context.SaveChanges();
            _context.Advertisements.Add(new Advertisement
            {
                CategoryId = 1,
                Content = "Lal123al",
                Cost = 2.2m,
                IsActive = true,
                Name ="Nam123e",
                Type = AdType.BannerAd,
                ViewsCount = 1

            });
            _context.SaveChanges();
            return Ok();
        }
    }
}
