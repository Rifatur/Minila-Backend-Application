using Microsoft.AspNetCore.Mvc;
using Minila.Web.Models;
using MinilaDataAcess.Model;
using Newtonsoft.Json;

namespace Minila.Web.Controllers
{
    public class ParentController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Parenlist()
        {
            
            List<ParentWebModel> list = new List<ParentWebModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            HttpResponseMessage response = await client.GetAsync("Parent/GetAllParent");
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<ParentWebModel>>(res);
            }
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> ParentDetails(long id)
        {

            Parent details = new Parent();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            HttpResponseMessage response = await client.GetAsync($"Parent/GetByParentID?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                details = JsonConvert.DeserializeObject<Parent>(res);
            }
            return View(details);
        }

        [HttpGet]
        public async Task<IActionResult> CreateParent()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            ViewBag.Date = currentDate;
            Random rnd = new Random();
            int myRandomNo = rnd.Next(10000000, 99999999);
            ViewBag.Code = "CR"+myRandomNo;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateParent(ParentWebModel parent )
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            var response = await client.PostAsJsonAsync<ParentWebModel>("Parent/AddParent", parent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Parenlist", "Parent");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {
            return View();
        }

    }
}
