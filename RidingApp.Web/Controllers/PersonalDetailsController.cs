using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RidingApp.DataAccess.Entities;
using RidingApp.Web.DTOs.ViewModels;

namespace RidingApp.Web.Controllers
{
    public class PersonalDetailsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<PersonalDetailsWebDTOs> Detailslist = new List<PersonalDetailsWebDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/PersonalDetails/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    Detailslist = JsonConvert.DeserializeObject<List<PersonalDetailsWebDTOs>>(apiRespose);
                }
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateDetails(PersonalDetailsWebDTOs request)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.PostAsJsonAsync<PersonalDetailsWebDTOs>("PersonalDetails/Create", request);
            if (response.IsSuccessStatusCode)
            {
                TempData["RequestAlertMessage"] = "Your Request Send Successfully ..!";
                return RedirectToAction("Index", "Admin");

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRequest(PersonalDetails request)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.PutAsJsonAsync<PersonalDetails>("PersonalDetails/Update", request);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "Admin");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteInfo(long id)
        {
            PersonalDetailsWebDTOs DeleteRideRequest = new PersonalDetailsWebDTOs();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.DeleteAsync($"PersonalDetails/Delete?id={id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["alertDeleteMessage"] = "Deleted Enable Road Successfully ..!";
                return RedirectToAction("index", "Admin");
            }
            return View();

        }

    }
}
