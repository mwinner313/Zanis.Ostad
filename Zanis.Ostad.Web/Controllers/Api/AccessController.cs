using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Zanis.Ostad.Web.Controllers.Api
{
    public class AccessController:Controller
    {
        [Route("access-unauthorized")]
        [HttpGet]
        public  ActionResult Get()
        {
            return StatusCode(403);
        }
    }
}
