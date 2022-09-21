using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RidingApp.DataAccess;
using RidingApp.DataAccess.Entities;
using RidingApp.Web.DTOs.ViewModels;

namespace RidingApp.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _DbContext;
        public ReportController(

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
            /// <summary>
            /// Geting TripRequest By Today........!
            /// </summary>
            /// 
            List<TripRequestWebDTOs> TripRequestByToday = new List<TripRequestWebDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/TripRequest/GetRequestByToday"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    TripRequestByToday = JsonConvert.DeserializeObject<List<TripRequestWebDTOs>>(apiRespose);
                }
            }
            ViewData["TripRequestByToday"] = TripRequestByToday;
            /// <summary>
            /// Geting TripRequest By Today........
            /// </summary>
            List<TripRequestWebDTOs> TripRequestlist = new List<TripRequestWebDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/TripRequest/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    TripRequestlist = JsonConvert.DeserializeObject<List<TripRequestWebDTOs>>(apiRespose);
                }
            }
            ViewData["TotalTripRequest"] = TripRequestlist;
            /// <summary>
            /// Geting TotalTrip Request........
            /// </summary>


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
            /// Geting Today Trip .......
            /// </summary>
            /// 
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
            /// <summary>
            /// Geting Total Trip .......
            /// </summary>

            /// <summary>
            /// Geting Total Student .......
            /// </summary>
            List<ApplicationUser> getstudentList = new List<ApplicationUser>();
            var Users = await _DbContext.ApplicationUser.ToListAsync();
            var UserInStudentRole = await (from user in _DbContext.Users
                                           join userRole in _DbContext.UserRoles
                                           on user.Id equals userRole.UserId
                                           join role in _DbContext.Roles
                                           on userRole.RoleId equals role.Id
                                           where role.Name == "Student"
                                           select user).ToListAsync();


            foreach (var item in UserInStudentRole)
            {
                getstudentList.Add(new ApplicationUser
                {
                    Id = item.Id,
                    UserName = item.UserName,
                });
            }
            ViewData["getstudentList"] = getstudentList;
            /// <summary>
            /// Geting Total Driver .......
            /// </summary>
            List<ApplicationUser> getDriverList = new List<ApplicationUser>();
            var DriverUsers = await _DbContext.ApplicationUser.ToListAsync();
            var UserInDriverRole = await (from user in _DbContext.Users
                                          join userRole in _DbContext.UserRoles
                                          on user.Id equals userRole.UserId
                                          join role in _DbContext.Roles
                                          on userRole.RoleId equals role.Id
                                          where role.Name == "User"
                                          select user).ToListAsync();


            foreach (var item in UserInDriverRole)
            {
                getDriverList.Add(new ApplicationUser
                {
                    Id = item.Id,
                    UserName = item.UserName,
                });
            }
            ViewData["getDriverList"] = getDriverList;
            /// <summary>
            /// Geting Total Driver .......
            /// </summary>
            List<ApplicationUser> getAdminList = new List<ApplicationUser>();
            var AdminUsers = await _DbContext.ApplicationUser.ToListAsync();
            var UserInAdminRole = await (from user in _DbContext.Users
                                         join userRole in _DbContext.UserRoles
                                         on user.Id equals userRole.UserId
                                         join role in _DbContext.Roles
                                         on userRole.RoleId equals role.Id
                                         where role.Name == "User"
                                         select user).ToListAsync();


            foreach (var item in UserInDriverRole)
            {
                getAdminList.Add(new ApplicationUser
                {
                    Id = item.Id,
                    UserName = item.UserName,
                });
            }
            ViewData["getAdminList"] = getAdminList;


            return View();

        }
    }
}
