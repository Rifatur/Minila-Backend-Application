using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RidingApp.DataAccess;

namespace RidingApp.Web.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _DbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        public ManageController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext DbContext,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _DbContext = DbContext;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var UserList = _DbContext.ApplicationUser.ToList();
            var userRole = _DbContext.UserRoles.ToList();
            var roles = _DbContext.Roles.ToList();
            foreach (var User in UserList)
            {
                var role = userRole.FirstOrDefault(x => x.UserId == User.Id);
                if (role == null)
                {
                    User.Role = "None";

                }
                else
                {
                    User.Role = roles.FirstOrDefault(u => u.Id == role.RoleId).Name;
                }
            }

            return View(UserList);
        }

        public IActionResult UpdateUser(string userId)
        {
            var GetUserById = _DbContext.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            if (GetUserById == null)
            {
                return NotFound();
            }
            var userRole = _DbContext.UserRoles.ToList();
            var GetRoles = _DbContext.Roles.ToList();
            var role = userRole.FirstOrDefault(x => x.UserId == GetUserById.Id);
            if (role != null)
            {
                GetUserById.RoleId = GetRoles.FirstOrDefault(u => u.Id == role.RoleId).Id;
            }
            GetUserById.RoleList = _DbContext.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            return View(GetUserById);
        }


    }
}
