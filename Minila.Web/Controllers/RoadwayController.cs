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
            List<RoadWayWebModel> RWlist = new List<RoadWayWebModel>();
            using(var httpClient = new HttpClient())
            {
                using(var getResponse = await httpClient.GetAsync("https://localhost:7211/Roadway/GetRoadway"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    RWlist = JsonConvert.DeserializeObject<List<RoadWayWebModel>>(apiRespose);
                }
            }
            ViewBag.TotalRoadwaylist = RWlist.Count();

            return View(RWlist);

        }


        //Create RoadWay....
        [HttpPost]
        public async Task<IActionResult> CreateRoadWay(RoadWayWebModel roadWay)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            var response = await client.PostAsJsonAsync<RoadWayWebModel>("RoadWay/AddRoadWay", roadWay);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "RoadWay");
            }
            return View();
        }



    }
}
