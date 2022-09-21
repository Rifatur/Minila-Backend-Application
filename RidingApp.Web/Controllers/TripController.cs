using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RidingApp.DataAccess;
using RidingApp.DataAccess.Entities;
using RidingApp.Web.DTOs.ViewModels;

namespace RidingApp.Web.Controllers
{
    public class TripController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _DbContext;
        public TripController(

            UserManager<IdentityUser> userManager,
            ApplicationDbContext DbContext,
            RoleManager<IdentityRole> roleManager)

        {
            _userManager = userManager;
            _DbContext = DbContext;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            List<TripWebDTOs> Triplist = new List<TripWebDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/Trips/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    Triplist = JsonConvert.DeserializeObject<List<TripWebDTOs>>(apiRespose);
                }
            }
            ViewData["TotalTrip"] = Triplist;

            List<TripWebDTOs> TotalTodayTrip = new List<TripWebDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/Trips/GetTriptByToday"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    TotalTodayTrip = JsonConvert.DeserializeObject<List<TripWebDTOs>>(apiRespose);
                }
            }
            ViewData["TotalTodayTrip"] = TotalTodayTrip;

            /// <summary>
            /// Geting Total Trip .......
            /// </summary>
            List<ApplicationUser> UserList = new List<ApplicationUser>();
            var Users = await _DbContext.ApplicationUser.ToListAsync();
            foreach (var item in Users)
            {
                UserList.Add(new ApplicationUser
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                });
            }
            ViewData["User"] = UserList;



            return View(Triplist);

        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip(TripWebDTOs request)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.PostAsJsonAsync<TripWebDTOs>("Trips/Create", request);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Trip", "Admin");
            }
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> UpdateTrip(TripWebDTOs request)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.PutAsJsonAsync<TripWebDTOs>("Trips/Update", request);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("TripRequest", "Admin");
            }
            return View();
        }



    }
}
