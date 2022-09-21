using Microsoft.AspNetCore.Mvc;
using RidingApp.DataAccess;
using RidingApp.Web.DTOs.ViewModels;

namespace RidingApp.Web.Controllers
{
    public class TripRequestController : Controller
    {
        private readonly ApplicationDbContext _DbContext;
        public TripRequestController(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public IActionResult Index()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            ViewBag.Date = currentDate;

            DateTime now = DateTime.Now;
            DateTime utc = DateTime.UtcNow;
            ViewBag.time = now.ToString("T");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateTripRequest(TripRequestWebDTOs request)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.PostAsJsonAsync<TripRequestWebDTOs>("TripRequest/Create", request);
            if (response.IsSuccessStatusCode)
            {
                TempData["RequestAlertMessage"] = "Your Request Send Successfully ..!";
                return RedirectToAction("index", "Admin");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateTripRequest(TripRequestWebDTOs request)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.PutAsJsonAsync<TripRequestWebDTOs>("TripRequest/Update", request);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("TripRequest", "Admin");
            }
            return View();
        }


    }
}
