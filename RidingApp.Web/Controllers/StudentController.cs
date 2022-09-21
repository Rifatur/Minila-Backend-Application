using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RidingApp.DataAccess;
using RidingApp.DataAccess.Entities;

namespace RidingApp.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _DbContext;
        public StudentController(

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
            List<ApplicationUser> UserList = new List<ApplicationUser>();
            var Student = await _DbContext.ApplicationUser.ToListAsync();
            foreach (var item in Student)
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


            return View(getstudentList);
        }

        [HttpGet]
        public async Task<IActionResult> StudentDetails(string Id)
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

            var chauffeurTripList = await _DbContext.Trip.Where(t => t.StudetID == Id).ToListAsync();
            ViewData["studentTripList"] = chauffeurTripList;
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