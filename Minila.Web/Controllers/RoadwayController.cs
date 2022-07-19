using Microsoft.AspNetCore.Mvc;
using Minila.Web.Models;
using Newtonsoft.Json;

namespace Minila.Web.Controllers
{
    public class RoadwayController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //getting School List .. 
            List<SchoolWebModel> Schoollist = new List<SchoolWebModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            HttpResponseMessage response = await client.GetAsync("School/GetSchool");
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                Schoollist = JsonConvert.DeserializeObject<List<SchoolWebModel>>(res);
            }
            ViewData["school"] = Schoollist;

            //getting Random number
            Random rnd = new Random();
            int myRandomNo = rnd.Next(100000, 999999);
            ViewBag.RW = "RW" + myRandomNo;

            //getting All  List Of Road Way ... 
            List<RoadWayWebModel> Roadwaylist = new List<RoadWayWebModel>();
            HttpClient clientRW = new HttpClient();
            clientRW.BaseAddress = new Uri("https://localhost:7211/");
            HttpResponseMessage RW_response = await clientRW.GetAsync("Roadway/GetRoadway");
            if (RW_response.IsSuccessStatusCode)
            {
                var result = RW_response.Content.ReadAsStringAsync().Result;
                Roadwaylist = JsonConvert.DeserializeObject<List<RoadWayWebModel>>(result);
            }
            ViewBag.TotalRoadwaylist = Roadwaylist.Count();

            return View(Roadwaylist);

        }


        //Create RoadWay....
        [HttpPost]
        public async Task<IActionResult> CreateRoadWay(RoadWayWebModel roadWay)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            var response = await client.PostAsJsonAsync<RoadWayWebModel>("School/AddSchool", roadWay);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "RoadWay");
            }
            return View();
        }



    }
}
