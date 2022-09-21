using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RidingApp.DataAccess;
using RidingApp.Web.DTOs.Requests;

namespace RidingApp.Web.Controllers
{
    public class RoadWayController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _DbContext;
        public RoadWayController(

            UserManager<IdentityUser> userManager,
            ApplicationDbContext DbContext,
            RoleManager<IdentityRole> roleManager)

        {
            _userManager = userManager;
            _DbContext = DbContext;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoad(RoadWayDTOs model)
        {
            var Users = await _DbContext.ApplicationUser.FirstOrDefaultAsync(x => x.Email == model.CreatedBy);
            var UserInStudentRole = await (from user in _DbContext.Users
                                           join userRole in _DbContext.UserRoles
                                           on user.Id equals userRole.UserId
                                           join role in _DbContext.Roles
                                           on userRole.RoleId equals role.Id
                                           where role.Name == "Admin"
                                           select user).FirstOrDefaultAsync();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.PostAsJsonAsync<RoadWayDTOs>("RoadWay/Create", model);
            if (response.IsSuccessStatusCode)
            {
                if (UserInStudentRole != null)
                {
                    TempData["alertMessage"] = "Road Added Successfully ..!";
                    return RedirectToAction("Roadways", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "RoadWay");
                }

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRoad(RoadWayDTOs request)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.PutAsJsonAsync<RoadWayDTOs>("RoadWay/Update", request);
            if (response.IsSuccessStatusCode)
            {
                TempData["alertMessage"] = "Road Edited Successfully ..!";
                return RedirectToAction("Roadways", "Admin");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRoad(long id)
        {
            RoadWayDTOs DeleteRideRequest = new RoadWayDTOs();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.DeleteAsync($"RoadWay/Delete?id={id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["alertDeleteMessage"] = "Road Deleted Successfully ..!";
                return RedirectToAction("Roadways", "Admin");
            }
            return View();

        }


    }
}
