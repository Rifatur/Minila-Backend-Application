using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RidingApp.DataAccess;
using RidingApp.DataAccess.Entities;
using RidingApp.Web.DTOs.ViewModels;

namespace RidingApp.Web.Controllers
{
    public class ChauffeurController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _DbContext;
        public ChauffeurController(

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
            List<ApplicationUser> getChauffeurList = new List<ApplicationUser>();
            var Users = await _DbContext.ApplicationUser.ToListAsync();
            var UserInStudentRole = await (from user in _DbContext.Users
                                           join userRole in _DbContext.UserRoles
                                           on user.Id equals userRole.UserId
                                           join role in _DbContext.Roles
                                           on userRole.RoleId equals role.Id
                                           where role.Name == "User"
                                           select user).ToListAsync();


            foreach (var item in UserInStudentRole)
            {

                getChauffeurList.Add(new ApplicationUser
                {
                    Id = item.Id,
                    UserName = item.UserName,
                });
            }

            ViewData["getDriverList"] = getChauffeurList;

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

            return View(getChauffeurList);
        }
        [HttpGet]
        public async Task<IActionResult> ChauffeurDetails(string Id)
        {
            ApplicationUser chauffeur = new ApplicationUser();
            var result = await _DbContext.ApplicationUser.FirstOrDefaultAsync(u => u.Id == Id);
            if (result! == null)
            {
                chauffeur.Id = result.Id;
                chauffeur.UserName = result.UserName;
                chauffeur.FirstName = result.FirstName;
                chauffeur.LastName = result.LastName;
                chauffeur.PhoneNumber = result.PhoneNumber;
            }
            var chauffeurTripListcount = await _DbContext.Trip.Where(t => t.DriverId == Id).ToListAsync();
            ViewBag.chauffeurTripListcount = chauffeurTripListcount.Count();

            var chauffeurTripList = await _DbContext.Trip.Where(t => t.DriverId == Id).ToListAsync();
            ViewData["chauffeurTripList"] = chauffeurTripList;
            //User Details
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

            return View(result);
        }

    }
}
