using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RidingApp.DataAccess;
using RidingApp.DataAccess.Entities;
using RidingApp.Web.DTOs.ViewModels;
using RidingApp.Web.Models;
using System.Diagnostics;

namespace RidingApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _DbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        public HomeController(
            ILogger<HomeController> logger,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext DbContext,
            SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _DbContext = DbContext;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<ActionResult> Index()
        {
            var UserList = _DbContext.ApplicationUser.ToList();
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

            var TotalUsers = await _DbContext.ApplicationUser.ToListAsync();
            ViewData["TotalUsers"] = TotalUsers;
            return View();
        }

        [Route("u/{username}")]
        public async Task<ActionResult> GetProfile(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
                return NotFound("User not found");

            return View(new UserViewWebDTOs
            {
                Id = user.Id,
                UserName = user.UserName,
            });
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}