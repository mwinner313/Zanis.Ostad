using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Zanis.Ostad.Web.Controllers.Api
{
    [Authorize(Roles = "Admin,Editor")]
    [Route("api/[controller]")]
    public class EditorAccountController : Controller
    {
       // public async Task<ActionResult<PagenatedList<>>> Get()
    }
}
