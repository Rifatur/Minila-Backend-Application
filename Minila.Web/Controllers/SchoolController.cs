using Microsoft.AspNetCore.Mvc;
using Minila.Web.Models;
using Newtonsoft.Json;

namespace Minila.Web.Controllers
{
    public class SchoolController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<SchoolWebModel> Schoollist = new List<SchoolWebModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            HttpResponseMessage response = await client.GetAsync("School/GetSchool");
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                Schoollist = JsonConvert.DeserializeObject<List<SchoolWebModel>>(res);
            }
            ViewBag.TotalSchoollist = Schoollist.Count();
            return View(Schoollist);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchool(SchoolWebModel school)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            var response = await client.PostAsJsonAsync<SchoolWebModel>("School/AddSchool", school);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "School");
            }
            return View();
        }

    }
}
