using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/slider")]
    public class SliderController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<List<SliderViewModel>>> Index()
        {
            return Ok(new List<SliderViewModel>
            {
                new SliderViewModel
                {
                    Id = 1,
                    Link = "http://www.link.com",
                    ImageUrl = "/images/android-slider.jpeg"
                },
                new SliderViewModel
                {
                    Id = 2,
                    Link = "http://www.link2.com",
                    ImageUrl = "/images/android-slider-2.jpeg"
                }
            });

        }
    }

    public class SliderViewModel
    {
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public int Id { get; set; }
    }
}
