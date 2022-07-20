using Microsoft.AspNetCore.Mvc;

namespace Minila.Web.Controllers
{
    public class FindRiderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
