using Microsoft.AspNetCore.Mvc;
using Minila.Web.Models;
using MinilaDataAcess.Model;
using Newtonsoft.Json;

namespace Minila.Web.Controllers
{
    public class ChauffeurController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<ChauffeurWebModel> list = new List<ChauffeurWebModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            HttpResponseMessage response = await client.GetAsync("Chauffeur/GetChauffeurs");
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<ChauffeurWebModel>>(res);
            }
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Chauffeurlist()
        {

            List<ChauffeurWebModel> list = new List<ChauffeurWebModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            HttpResponseMessage response = await client.GetAsync("Chauffeur/GetChauffeurs");
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<ChauffeurWebModel>>(res);
            }
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> ChauffeurDetails(long id)
        {

            Chauffeur details = new Chauffeur();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            HttpResponseMessage response = await client.GetAsync($"Chauffeur/GetByChauffeurID?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                details = JsonConvert.DeserializeObject<Chauffeur>(res);
            }
            //getting All  List Of Road Way ... 
            List<RoadWayWebModel> RWlist = new List<RoadWayWebModel>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7211/Roadway/GetRoadway"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    RWlist = JsonConvert.DeserializeObject<List<RoadWayWebModel>>(apiRespose);
                }
            }
            ViewData["Roadlist"] = RWlist;

            //getting School List .. 
            List<SchoolWebModel> Schoollist = new List<SchoolWebModel>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7211/School/GetSchool"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    Schoollist = JsonConvert.DeserializeObject<List<SchoolWebModel>>(apiRespose);
                }
            }
            ViewData["school"] = Schoollist;

            //getting List Of Ride Request .. 
            List<FindRider> RideRequestlist = new List<FindRider>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync($"https://localhost:7211/Chauffeur/GetChauffeurRoad?ChauffeurId={id}"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    RideRequestlist = JsonConvert.DeserializeObject<List<FindRider>>(apiRespose);
                }
            }
            ViewData["ChauffeurRequest"] = RideRequestlist;

            //Getting Time 
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            ViewBag.Date = currentDate;

            //getting List Of Ride Request .. 
            List<TripRequest> ChauffeurTripRequest = new List<TripRequest>();
            var RiderStringID = id.ToString();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync($"https://localhost:7211/TripRequest/GetRequestByChauffeur?ChauffeurID={RiderStringID}"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    ChauffeurTripRequest = JsonConvert.DeserializeObject<List<TripRequest>>(apiRespose);
                }
            }
            ViewData["TripRequest"] = ChauffeurTripRequest;

            return View(details);
        }
        //#Edit Trip Request By Rider#
        [HttpGet]
        public async Task<IActionResult> EditTripRequest(long id)
        {
            TripRequest tripRequest = new TripRequest();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync($"https://localhost:7211/TripRequest/GetSingleTripRequest?id={id}"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    tripRequest = JsonConvert.DeserializeObject<TripRequest>(apiRespose);
                }
                return View(tripRequest);
            }
        }
        //update
        [HttpPost]
        public async Task<IActionResult> EditTripRequest(TripRequest request)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            var response = await client.PutAsJsonAsync<TripRequest>("TripRequest/UpdateTripRequest", request);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "Chauffeur");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateChauffeur()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            ViewBag.Date = currentDate;
            Random rnd = new Random();
            int myRandomNo = rnd.Next(10000000, 99999999);
            ViewBag.Code = "CR" + myRandomNo;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateChauffeur(ChauffeurWebModel chauffeur)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            var response = await client.PostAsJsonAsync<ChauffeurWebModel>("Chauffeur/AddChauffeur", chauffeur);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "Chauffeur");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFindSearch(FindRiderWebModel findRider)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            var response = await client.PostAsJsonAsync<FindRiderWebModel>("FindRider/AddFindRider", findRider);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "Chauffeur");
            }
            return View();
        }



    }
}
