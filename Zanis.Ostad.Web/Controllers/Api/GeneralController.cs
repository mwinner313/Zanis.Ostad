using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Zains.Ostad.Application.General.SearchInFieldsAndLessons;
using Zanis.Ostad.Core.Entities.Cart;
using Zanis.Ostad.Web.Models;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class GeneralController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public GeneralController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }
        [HttpGet("Search")]
        public async Task<ActionResult<List<SearchResult>>> Search([FromQuery]SearchInFieldsAndLessonsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpGet("AndroidAppVersion")]
        public async Task<ActionResult> AndroidAppVersion()
        {
            return Ok(new Version{ VersionNumber= _configuration["androidAppVersion"]});
        }
        [HttpGet("GetHomePageDecorData")]
        public async Task<ActionResult> GetHomePageDecorData()
        {
            return Ok(new List<DecorData>
            {
                new DecorData {
                    Title = "پر بازدید ترین ها",
                    Items = new List<DecorDataItem>
                    {
                        new DecorDataItem
                        {
                            Id = 27,
                            Type = ProductType.LessonExam,
                            Title = "انقلاب اسلامی ایران",
                            Price = 100000,
                            OffPercentage = 20
                        }
                    }
                }
            });
        }

    }

    public class Version
    {
        public string VersionNumber { get; set; }
    }
}
