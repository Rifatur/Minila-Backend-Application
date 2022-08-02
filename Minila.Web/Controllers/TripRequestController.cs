using Microsoft.AspNetCore.Mvc;
using MinilaDataAcess.Model;

namespace Minila.Web.Controllers
{
    public class TripRequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //Create Ride Request....
        [HttpGet]
        public async Task<IActionResult> SingleTripRequest(long id)
        {

            return PartialView(nameof(SingleTripRequest), id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTripRequest(TripRequest request)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            var response = await client.PostAsJsonAsync<TripRequest>("TripRequest/CreateTripRequest", request);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("StudentDetails", "Student", new { @id = request.StudetID });
            }
            return View();
        }
        //update
        [HttpPut]
        public async Task<IActionResult> UpdateTripRequest(TripRequest request)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            var response = await client.PutAsJsonAsync<TripRequest>("TripRequest/UpdateTripRequest", request);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ChauffeurDetails", "Chauffeur", new { @id = request.ChauffeurId });
            }
            return View();
        }


    }
}
