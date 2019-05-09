using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace Zanis.Ostad.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
