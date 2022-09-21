using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RidingApp.DataAccess;
using RidingApp.Web.DTOs.ViewModels;

namespace RidingApp.Web.Controllers
{
    public class SchoolController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _DbContext;
        public SchoolController(

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
            List<SchoolWebDTOs> Schoollist = new List<SchoolWebDTOs>();
            //getting All  List Of ... 
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/School/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    Schoollist = JsonConvert.DeserializeObject<List<SchoolWebDTOs>>(apiRespose);
                }
            }
            //Getting Time 
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            ViewBag.Date = currentDate;
            ViewBag.TotalSchoollist = Schoollist.Count();
            return View(Schoollist);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchool(SchoolWebDTOs school)
        {
            var Users = await _DbContext.ApplicationUser.FirstOrDefaultAsync(x => x.Email == school.CreatedBy);
            var UserInStudentRole = await (from user in _DbContext.Users
                                           join userRole in _DbContext.UserRoles
                                           on user.Id equals userRole.UserId
                                           join role in _DbContext.Roles
                                           on userRole.RoleId equals role.Id
                                           where role.Name == "Student"
                                           select user).FirstOrDefaultAsync();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.PostAsJsonAsync<SchoolWebDTOs>("School/CreateSchool", school);
            if (response.IsSuccessStatusCode)
            {
                if (UserInStudentRole != null)
                {
                    TempData["alertMessage"] = "School Added Successfully ..!";
                    return RedirectToAction("School", "Admin");
                }
                else
                {
                    return RedirectToAction("index", "School");
                }

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSchool(SchoolWebDTOs school)
        {
            var Users = await _DbContext.ApplicationUser.FirstOrDefaultAsync(x => x.Email == school.CreatedBy);
            var UserInStudentRole = await (from user in _DbContext.Users
                                           join userRole in _DbContext.UserRoles
                                           on user.Id equals userRole.UserId
                                           join role in _DbContext.Roles
                                           on userRole.RoleId equals role.Id
                                           where role.Name == "Student"
                                           select user).FirstOrDefaultAsync();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.PutAsJsonAsync<SchoolWebDTOs>("School/Update", school);
            if (response.IsSuccessStatusCode)
            {
                if (UserInStudentRole != null)
                {
                    TempData["alertDeleteMessage"] = "School Deleted Successfully ..!";
                    return RedirectToAction("School", "Admin");
                }
                else
                {
                    return RedirectToAction("index", "School");
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSchool(long id)
        {
            SchoolWebDTOs DeleteRideRequest = new SchoolWebDTOs();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7006/");
            var response = await client.DeleteAsync($"School/Delete?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("School", "Admin");
            }
            return View();

        }

    }
}
