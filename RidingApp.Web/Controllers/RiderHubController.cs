using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RidingApp.Web.DTOs.ViewModels;

namespace RidingApp.Web.Controllers
{
    public class RiderHubController : Controller
    {
        public async Task<IActionResult> Index()
        {

            //getting List Of Ride Request .. 
            List<RiderHubDTOs> RideRequestlist = new List<RiderHubDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync($"https://localhost:7006/RiderHub/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    RideRequestlist = JsonConvert.DeserializeObject<List<RiderHubDTOs>>(apiRespose);
                }
            }
            ViewData["RiderHubListed"] = RideRequestlist;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFindRider(RiderHubDTOs model)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.PostAsJsonAsync<RiderHubDTOs>("RiderHub/Create", model);
            if (response.IsSuccessStatusCode)
            {
                TempData["alertMessage"] = "Road Enable Successfully ..!";
                return RedirectToAction(nameof(AdminController.Index), "Admin");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteFindRider(long id)
        {
            RiderHubDTOs DeleteRideRequest = new RiderHubDTOs();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.DeleteAsync($"RiderHub/Delete?id={id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["alertDeleteMessage"] = "Deleted Enable Road Successfully ..!";
                return RedirectToAction("EnableRoad", "Admin");
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> UpdateRider(RiderHubDTOs request)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.PutAsJsonAsync<RiderHubDTOs>("RiderHub/Update", request);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("EnableRoad", "Admin");
            }
            return View();
        }





    }
}
