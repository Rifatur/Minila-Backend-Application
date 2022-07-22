using Microsoft.AspNetCore.Mvc;
using Minila.Web.Models;

namespace Minila.Web.Controllers
{
    public class FindRiderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateFindSearch(FindRiderWebModel findRider)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            var response = await client.PostAsJsonAsync<FindRiderWebModel>("FindRider/AddFindRider", findRider);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "FindRider");
            }
            return View();
        }
    }
}
